using Core.Entities.Concrete;

namespace Business.Constants;

public static class Messages
{
    public const string ProductAdded = "Product added.";
    public const string ProductNameInvalid = "Product name invalid.";
    public const string ProductsListed = "Products listed.";
    public const string MaintenanceTime = "Maintenance time.";
    public const string ProductCountOfCategoryError = "In a category, there can be at most 10 products.";
    public const string ProductNameAlreadyExists = "Product name already exists.";
    public const string ProductUpdated = "Product updated.";
    public const string CategoryLimitExceeded = "Category limit exceeded.";
    public const string AuthorizationDenied = "Authorization denied.";
    public const string UserAdded = "User added.";
    public const string UserRegistered = "User registered.";
    public const string UserNotFound = "User not found.";
    public const string PasswordError = "Password error.";
    public const string SuccessfulLogin = "Successful login.";
    public const string UserAlreadyExists = "User already exists.";
    public const string AccessTokenCreated = "Access token created.";
}