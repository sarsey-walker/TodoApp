﻿// Ignore Spelling: App

using SQLite;
using System.Diagnostics;
using TodoApp.Data;

namespace TodoApp.Repository
{
    public class DailynoteRepository
    {

        private string _dbPath;
        private SQLiteAsyncConnection _conn;

        public string StatusMessage { get; set; }

        public DailyNote dailyNote { get; set; }

        private void Init()
        {
            // Initialize the repository         
            if (_conn != null)
                return;

            _conn = new SQLiteAsyncConnection(_dbPath);
            _ = _conn.CreateTableAsync<DailyNote>();
        }

        public DailynoteRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task AddNewEntryAsync(string text, DateTime date)
        {
            int result = 0;
            try
            {
                Init();

                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(text))
                    throw new Exception("Valid text required");

                // Insert the new data into the database
                result = await _conn.InsertAsync(new DailyNote { Text = text, Time = date });

                StatusMessage = string.Format("{0} record(s) added.", result);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add. Error: {0}", ex.Message);
            }

        }

        public async Task<List<DailyNote>> GetAllNotesAsync()
        {
            try
            {
                Init();
                return await _conn.Table<DailyNote>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return await Task.FromResult(new List<DailyNote>());
        }

        public async Task<DailyNote> GetNoteAsync(DateTime date)
        {
            try
            {
                Init();
                var notes = await _conn.QueryAsync<DailyNote>("SELECT * FROM DailyNote WHERE TIME = ?", date);
                if (notes.Any())
                    return notes.FirstOrDefault();
                return new DailyNote();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
                return new DailyNote();
            }
        }
        //public async Task<DailyNote> GetNote()
        //{
        //    return 
        //}

        public async Task<DailyNote> GetNoteByIdAsync(int id)
        {
            try
            {
                Init();
                var notes = await _conn.QueryAsync<DailyNote>("SELECT * FROM DailyNote WHERE ID = ?", id);
                if (notes.Any())
                    return notes.FirstOrDefault();
                return new DailyNote();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
                return new DailyNote();
            }
        }


        public async Task UpdateNoteAsync(DailyNote todo)
        {
            int result = 0;
            try
            {
                Init();

                // basic validation to ensure a name was entered
                result = await _conn.UpdateAsync(todo);

                StatusMessage = string.Format("{0} record(s) Updated", result);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to update. Error: {0}", ex.Message);
            }

        }

        public async Task DeleteNoteAsync(DailyNote todo)
        {
            int result = 0;
            try
            {
                Init();

                result = await _conn.DeleteAsync(todo);

                StatusMessage = string.Format("{0} record(s) deleted", result);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to delete. Error: {0}", ex.Message);
            }
        }
        public async Task<IEnumerable<DateTime>> GetDaysWithNotes(DateTime date)
        {
            try
            {
                Init();
                var dateIni = new DateTime(date.Year, date.Month, 1);
                var dateEnd = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                var query = _conn.Table<DailyNote>()
                    .Where(x => x.Time >= dateIni && x.Time <= dateEnd);

                var notes = query.ToListAsync().Result;

                if (!notes.Any())
                    return new List<DateTime>();

                return notes.Select(y => y.Time);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
                return null;
            }
        } 

    }
}