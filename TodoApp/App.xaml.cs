using TodoApp.Repository;

namespace TodoApp;

public partial class App : Application
{
    // TODO: Add a public static PersonRepository property
    public static TodoRepository TodoRepo { get; private set; }
    public App(TodoRepository repo)
	{
		InitializeComponent();

		MainPage = new MainPage();
        // TODO: Initialize the PersonRepository property with the PersonRespository singleton object

        TodoRepo = repo;
    }
}
