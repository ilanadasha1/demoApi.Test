# demoApi.Test
The project describe a demo WebApi (REST) automation framework structure with:
1. demoApi.Test.SDK - The API interface which provide abstraction layer to the test project.
2. demoApi.Test - The Tests layer which coverage API functionality

Note: Should refer to: https://github.com/ilanadasha1/demoApi as the REST API service (SUT)


Reporting:
1. In order to create allure report, install Allure for C# (nugets: Allure.Commons, NUnit.Allure)
2. Navigate to test result folder at: demoApi.Test\demoApi.Test\bin\Debug\netcoreapp3.0
3. Run: allure serve (you need to install allure with scoop package manager: https://scoop.sh/) 
