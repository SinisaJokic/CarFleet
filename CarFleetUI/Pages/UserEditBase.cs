using CarFleetModels;
using CarFleetUI.Services;
using Microsoft.AspNetCore.Components;

namespace CarFleetUI.Pages
{
    public class UserEditBase : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }
        [Parameter]
        public string UserId { get; set; }

        public UserModel UserModel { get; set; } = new UserModel();
        public string Message { get; set; }
        protected string StatusClass = string.Empty;
        protected bool Saved;


        protected override async Task OnInitializedAsync()
        {
            Saved = false;

            int.TryParse(UserId, out var userId);

            if (userId != 0) //new employee is being created
            {
                UserModel = await UserService.GetUserDetails(int.Parse(UserId));
            }
        }
        protected async Task HandleValidSubmit()
        {
            if (UserModel.PkUser == 0) //new
            {
                var addedUser = await UserService.AddUser(UserModel);

                if (addedUser != null)
                {
                    StatusClass = "alert-success";
                    Message = "New driver added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new driver. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await UserService.UpdateUser(UserModel);
                StatusClass = "alert-success";
                Message = "Driver updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteUser()
        {
            await UserService.DeleteUser(UserModel.PkUser);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }
        //protected void NavigateToOverview()
        //{
        //    NavigationManager.NavigateTo("/vehicleoverview");
        //}
    }
}
