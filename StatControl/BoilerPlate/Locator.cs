using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using StatControl.Mvvm.View;
using StatControl.Mvvm.ViewModel;
using StatControl.Services;
using StatControl.Services.Rest;
using FunctionZero.MvvmZero;
using SimpleInjector;
using System.Threading.Tasks;
using System.Diagnostics;
using StatControl.Constants;
using System.Net.Http;

namespace StatControl.BoilerPlate
{
	public class Locator
	{
		private readonly Container _IoCC;

		internal Locator(Application currentApplication)
		{
			// Create the IoC container that will contain all our configurable classes ...
			_IoCC = new Container();

			// Tell the IoC container what to do if asked for an IPageService
			_IoCC.Register<IPageServiceZero>(
				() =>
				{
					// This is how we create an instance of PageServiceZero.
					// The PageService needs to know how to get the current NavigationPage it is to interact with.
					// (If you have a FlyoutPage at the root, the navigationGetter should return the current Detail item)
					// It also needs to know how to get Page and ViewModel instances so we provide it with a factory
					// that uses the IoC container. We could easily provide any sort of factory, we don't need to use an IoC container.
					var pageService = new PageServiceZero(() => App.Current.MainPage.Navigation, (theType) => _IoCC.GetInstance(theType));
					return pageService;
				},
				// One only ever will be created.
				Lifestyle.Singleton
			);

			// Tell the IoC container about our Pages.
			_IoCC.Register<AchievementsPage>(Lifestyle.Singleton);
			_IoCC.Register<CarouselViewPage>(Lifestyle.Singleton);
			_IoCC.Register<FunPage>(Lifestyle.Singleton);
			_IoCC.Register<HomePage>(Lifestyle.Singleton);
			_IoCC.Register<IndividualWeaponPage>(Lifestyle.Singleton);
			_IoCC.Register<LoginPage>(Lifestyle.Singleton);
			_IoCC.Register<LastMatchPage>(Lifestyle.Singleton);
			_IoCC.Register<MainStatPage>(Lifestyle.Singleton);
			_IoCC.Register<MapPage>(Lifestyle.Singleton);
			_IoCC.Register<WeaponsSelectPage>(Lifestyle.Singleton);



			// Tell the IoC container about our ViewModels.
			_IoCC.Register<AchievementsPageVm>(Lifestyle.Singleton);
			_IoCC.Register<CarouselPageVm>(Lifestyle.Singleton);
			_IoCC.Register<FunPageVm>(Lifestyle.Singleton);
			_IoCC.Register<HomePageVm>(Lifestyle.Singleton);
			_IoCC.Register<IndividualWeaponPageVm>(Lifestyle.Singleton);
			_IoCC.Register<LoginPageVm>(Lifestyle.Singleton);
			_IoCC.Register<LastMatchPageVm>(Lifestyle.Singleton);
			_IoCC.Register<MainStatPageVm>(Lifestyle.Singleton);
			_IoCC.Register<MapPageVm>(Lifestyle.Singleton);
			_IoCC.Register<WeaponsSelectPageVm>(Lifestyle.Singleton);


			// Tell the IoC container about our Services.
			_IoCC.Register<SteamGameStatsService>(GetSteamUserStatsService, Lifestyle.Singleton);
			_IoCC.Register<SteamUserProfileService>(GetSteamUserProfileService, Lifestyle.Singleton);
			_IoCC.Register<SteamUserAchievementsService>(GetSteamUserAchievementsService, Lifestyle.Singleton);
			_IoCC.Register<IRestService>(GetRestService, Lifestyle.Singleton);

		}

		/// <summary>
		/// This is called once during application startup
		/// </summary>
		internal async Task SetFirstPage()
		{
			// Create and assign a top-level NavigationPage.
			// If you use a FlyoutPage instead then its Detail item will need to be a NavigationPage
			// and you will need to modify the 'navigationGetter' provided to the PageServiceZero instance to 
			// something like this:
			// () => ((FlyoutPage)App.Current.MainPage).Detail.Navigation
			App.Current.MainPage = new NavigationPage();
			// Ask the PageService to assemble and present our HomePage ...
			await _IoCC.GetInstance<IPageServiceZero>().PushPageAsync<LoginPage, LoginPageVm>((vm) => {/* Optionally interact with the vm, e.g. to inject seed-data */ });
		}

		/// <summary>
		/// For debug purposes to let us know when a Page is assembled by the PageService
		/// </summary>
		/// <param name="thePage">A reference to the page that has been presented</param>
		private void PageCreated(Page thePage)
		{
			Debug.WriteLine(thePage);
		}

		private IRestService GetRestService()
		{
			var httpClient = new HttpClient();
			// Configure the client.
			httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

			return new RestService(httpClient, ApiConstants.SteamBaseApiUrl);
		}

		private SteamGameStatsService GetSteamUserStatsService()
		{
			return new SteamGameStatsService(_IoCC.GetInstance<IRestService>(), ApiConstants.SteamGameStatEndpoint, ApiConstants.SteamApiKey);
		}

		private SteamUserProfileService GetSteamUserProfileService()
		{
			return new SteamUserProfileService(_IoCC.GetInstance<IRestService>(), ApiConstants.SteamUserProfileSummaryEndpoint, ApiConstants.SteamApiKey);
		}

		private SteamUserAchievementsService GetSteamUserAchievementsService()
		{
			return new SteamUserAchievementsService(_IoCC.GetInstance<IRestService>(), ApiConstants.SteamUserAchievementsEndpoint, ApiConstants.SteamApiKey);
		}

	}
}
