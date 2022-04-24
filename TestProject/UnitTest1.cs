using Bunit;
using Client.Pages.Public;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using Client.Components.Public.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Bunit.TestDoubles;

namespace TestProject
{
    public class UnitTest1


    {
        [Fact]
        
        public void RendersSuccessfully()
        {

            
            using var ctx = new TestContext();
            ctx.Services.AddBlazoredLocalStorage();
            ctx.Services.AddAuthorizationCore();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("TEST USER");
            authContext.SetRoles("Admin");

       

            var component = ctx.RenderComponent<NavB>();

            

            Assert.Equal("About", component.Find($".nav-item").TextContent);


        }

        [Fact]

        public void checkPage()
        {


            using var ctx = new TestContext();
            ctx.Services.AddBlazoredLocalStorage();
            ctx.Services.AddAuthorizationCore();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("TEST USER");
            authContext.SetRoles("Admin");



            var component = ctx.RenderComponent<Client.Pages.Public.ProductBook.Fleet>();

            Assert.Equal("Privacy", component.Find($"footer").TextContent);

            var buttonElement = component.Find("button");


            buttonElement.Click();


        }


        public void checkRenderPage()
        {
            using var ctx = new TestContext();
            ctx.Services.AddBlazoredLocalStorage();
            ctx.Services.AddAuthorizationCore();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("TEST USER");
            authContext.SetRoles("Admin");

            
            var navMan = ctx.Services.GetRequiredService<FakeNavigationManager>();
            var cut = ctx.RenderComponent<NavB>();

            // Act - trigger a navigation change
            navMan.NavigateTo("/register");

            // Assert - inspects markup to verify the location change is reflected there
            cut.Find("p").MarkupMatches($"<p>{navMan.BaseUri}/register</p>");
        }


    }
}