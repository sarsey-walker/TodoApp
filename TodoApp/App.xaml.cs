using TodoApp.Repository;

namespace TodoApp;

public partial class App : Application
{
    // Add a public static *Repository property
    public static TodoRepository TodoRepo { get; private set; }
    public static DailynoteRepositosy _notesRepo { get; private set; }
    public App(TodoRepository repo, DailynoteRepositosy dailynoteRepositosy)
	{
		InitializeComponent();

		MainPage = new MainPage();
        // TODO: Initialize the PersonRepository property with the PersonRespository singleton object

        TodoRepo = repo;
        _notesRepo = dailynoteRepositosy;
    }
}
