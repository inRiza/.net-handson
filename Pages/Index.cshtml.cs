using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoList.Models;

namespace TodoList.Pages;

public class IndexModel : PageModel
{
    private static List<TodoItem> _todos = new List<TodoItem>();
    public List<TodoItem> Todos { get; set; } = new List<TodoItem>();

    [BindProperty]
    public string NewTodoTitle { get; set; } = string.Empty;
    public void OnGet()
    {
        Todos = _todos.OrderByDescending(t => t.CreatedAt).ToList();
    }
    
    // Action posting item
    public IActionResult OnPostAdd()
    {
        if (!string.IsNullOrWhiteSpace(NewTodoTitle))
        {
            var newTodo = new TodoItem
            {
                Id = _todos.Count > 0 ? _todos.Max(t => t.Id) + 1 : 1,
                Title = NewTodoTitle,
                IsCompleted = false,
            };
            _todos.Add(newTodo);
        }

        return RedirectToPage();
    }

    public IActionResult OnPostToggleComplete(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo != null)
        {
            todo.IsCompleted = !todo.IsCompleted;
        }

        return RedirectToPage();
    }

    public IActionResult OnPostDelete(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo != null)
        {
            _todos.Remove(todo);
        }

        return RedirectToPage();
    }
}
