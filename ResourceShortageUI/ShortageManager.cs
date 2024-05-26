using ResourceShortageUI.Enums;
using ResourceShortageUI.Models;
using ResourceShortageUI.Repositories;

namespace ResourceShortageUI;

public class ShortageManager
{
    private readonly string? _currentUser;
    private readonly bool _isAdmin;
    private readonly ShortageRepository _repository;

    public ShortageManager(string? user, bool isAdmin, ShortageRepository repository)
    {
        _currentUser = user;
        _isAdmin = isAdmin;
        _repository = repository;
    }

    public string RegisterShortage(Shortage newShortage)
    {
        newShortage.UserName = _currentUser;
        var currentShortage = _repository.GetShortages().Find(s => s.Title == newShortage.Title
        && s.RoomType == newShortage.RoomType);

        if (currentShortage != null)
        {
            if (newShortage.Priority > currentShortage.Priority)
            {
                _repository.GetShortages().Remove(currentShortage);
                _repository.GetShortages().Add(newShortage);
                _repository.SaveShortage();
                return $"Current shortage updated with a new shrtage of a higher priority.";
            }
            else
            {
                return $"Shortage is already registered.";
            }
        }
        else
        {
            _repository.GetShortages().Add(newShortage);
            _repository.SaveShortage();
            return $"New shortage was registered.";
        }
    }

    public string DeleteShortageItem(string title, Room room)
    {
        var shortage = _repository.GetShortages().Find(s => s.Title == title
        && s.RoomType == room
        && (s.Name == _currentUser || _isAdmin));

        if (shortage != null)
        {
            _repository.GetShortages().Remove(shortage);
            _repository.SaveShortage();
            return $"Shortage item was deleted.";
        }
        else
        {
            return $"Shortage item was not found or you are not authorized to delete requested item.";
        }
    }

    public List<Shortage> ListShortageItems(string? filterTitle = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        Category? filterCategory = null,
        Room? filterRoom = null)
    {
        IEnumerable<Shortage> request = GetShortageForCurrentUser();

        if (!string.IsNullOrEmpty(filterTitle))
        {
            request = request.Where(s => s.Title.Contains(filterTitle, StringComparison.OrdinalIgnoreCase));
        }

        if (startDate.HasValue && endDate.HasValue)
        {
            request = request.Where(s => s.CreatedOn >= startDate.Value
            && s.CreatedOn <= endDate.Value);
        }

        if (filterCategory.HasValue)
        {
            request = request.Where(s => s.ItemCategory == filterCategory.Value);
        }

        if (filterRoom.HasValue)
        {
            request = request.Where(s => s.RoomType == filterRoom.Value);
        }

        return request.OrderByDescending(s => s.Priority).ToList();
    }

    private List<Shortage> GetShortageForCurrentUser()
    {
        if (_isAdmin)
        {
            return _repository.GetShortages();
        }
        else
        {
            return _repository.GetShortages().Where(s => s.UserName == _currentUser).ToList();
        }
    }
}
