﻿# dotenv-vault.net

[![NuGet version](https://badge.fury.io/nu/dotenv-vault.net.svg)](https://badge.fury.io/nu/dotenv-vault.net)
![Nuget downloads](https://img.shields.io/nuget/dt/dotenv-vault.net)

![Alt Text](https://raw.githubusercontent.com/motdotla/dotenv/master/dotenv.svg "dotenv-vault")

Extends the proven & trusted foundation of [dotenv](https://github.com/bolorundurowb/dotenv.net), with a `.env.vault` file.

The extended standard lets you sync your `.env` files – quickly & securely. Stop sharing them over insecure channels like Slack and email, and never lose an important `.env` file again.

## Installation

Follow these steps to install both of these libraries:
- dotenv-vault.net
- dotenv.net (3.1.0 <= version < 3.2.0)

If you want to add library reference manually then add this line to your `csproj` file:

```xml
<PackageReference Include="dotenv-vault.net" Version="0.0.1"/>
```

If you're using the Visual Studio package manager console, then run the following:

```cmd
Install-Package dotenv-vault.net
```

If you are making use of the dotnet CLI, then run the following in your terminal:

```bash
dotnet add package dotenv-vault.net
```

## Usage

Ensure you have declared the necessary namespace at the head of your class file:

```csharp
using dotenv_vault.net;
```

### Load Environment Variables

Calling the `Load()` method with no parameters would locate and load the `.env` file in the same directory that the library is if one exists:

```csharp
DotEnvVault.Load();
```

If you want to be notified of exceptions that occur in the process of loading env files then you can specify that via the configuration options:

```csharp
DotEnvVault.Load();
```
### Defaults: 

*The default is `.env`*

*The defaults are `true` and `4` directories up*

*The default encoding is `UTF-8`*

*The default to trim whitespaces is `false`*

*The default to skip overwriteing an environment variable is `true`*

## Dotenv.org

You need a [Dotenv Account](https://dotenv.org) to use Dotenv Vault. It is free to use with premium features.

![](https://api.checklyhq.com/v1/badges/checks/c2fee99a-38e7-414e-89b8-9766ceeb1927?style=flat&theme=dark&responseTime=true)
![](https://api.checklyhq.com/v1/badges/checks/4f557967-1ed1-486a-b762-39a63781d752?style=flat&theme=dark&responseTime=true)
<br>
![](https://api.checklyhq.com/v1/badges/checks/804eb6fa-6599-4688-a649-7ff3c39a64b9?style=flat&theme=dark&responseTime=true)
![](https://api.checklyhq.com/v1/badges/checks/6a94504e-e936-4f07-bc0b-e08fee2734b3?style=flat&theme=dark&responseTime=true)
<br>
![](https://api.checklyhq.com/v1/badges/checks/06ac4f4e-3e0e-4501-9987-580b4d2a6b06?style=flat&theme=dark&responseTime=true)
![](https://api.checklyhq.com/v1/badges/checks/0ffc1e55-7ef0-4c2c-8acc-b6311871f41c?style=flat&theme=dark&responseTime=true)

Visit [health.dotenv.org](https://health.dotenv.org) for more information.

## FAQ

### What happens if `DOTENV_KEY` is not set?

> Dotenv Vault gracefully falls back to [dotenv.net](https://github.com/bolorundurowb/dotenv.net) when `DOTENV_KEY` is not set. This is the default for development so that you can focus on editing your `.env` file and save the `build` command until you are ready to deploy those environment variables changes.

### Should I commit my `.env` file?

> No. We **strongly** recommend against committing your `.env` file to version control. It should only include environment-specific values such as database passwords or API keys. Your production database should have a different password than your development database.

### Should I commit my `.env.vault` file?

> Yes. It is safe and recommended to do so. It contains your encrypted envs, and your vault identifier.

### Can I share the `DOTENV_KEY`?

> No. It is the key that unlocks your encrypted environment variables. Be very careful who you share this key with. Do not let it leak.

## Contributing

1. Fork it
2. Create your feature branch (`git checkout -b my-new-feature`)
3. Commit your changes (`git commit -am 'Added some feature'`)
4. Push to the branch (`git push origin my-new-feature`)
5. Create new Pull Request

## Changelog

See [CHANGELOG.md](CHANGELOG.md)

## License

MIT