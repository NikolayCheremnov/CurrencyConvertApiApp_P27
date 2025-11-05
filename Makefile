
.PHONY: lint, test, build-windows

lint:
	dotnet format --verify-no-changes

test:
	dotnet test CurrencyConvertApiAppUnitTests/CurrencyConvertApiAppUnitTests.csproj --nologo

build-windows:
	dotnet publish CurrencyConvertApiApp/CurrencyConvertApiApp.csproj -c Release -r win-x64 --self-contained false -o bin/win-x64
