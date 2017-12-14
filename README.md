![Cache](https://github.com/thiagoloureiro/TestaCache/blob/master/cache-icon.png?raw=true)  

# TestaCache

#### .NET Component to implement cache in your application Cache using MemoryCache or Redis in C#

<img src="https://wiprojects.visualstudio.com/_apis/public/build/definitions/8ee69205-3d59-40e7-8207-9cc6a8fa8785/6/badge"/>

[![NuGet](https://buildstats.info/nuget/TestaCache)](http://www.nuget.org/packages/TestaCache)

This package use PostSharp Essentials, so it's limited to use up to 10 Classes with the attributes.
We are planning to use another AOP in the next version.

## How to Use:
Open Package Manager Console and run:

```Install-Package TestaCache```

## .NET Framework Support
- .NET Framework 4.6+
- .NET Core 1.1/2.0 will be supported in v1.1 (under development)

## Add the following keys for Redis Configuration on app.config
  ```
<appSettings>
    <add key="RedisCache_server" value="xxx" />
    <add key="RedisCache_port" value="9312" />
    <add key="RedisCache_name" value="redistogo" />
    <add key="RedisCache_password" value="xxxx" />
    <add key="RedisCache_ssl" value="false" />
</appSettings>
  ```
  

## Usage with Redis
Create the Attribute **[RedisCacheableResult]** for Redis usage, in case of Redis the type of **List** must be dynamic, this version doesn't support typed objects for Redis
So use ```List<dynamic>```

#### Code Sample

``` 
[RedisCacheableResult]
public List<dynamic> ReturnCustomer()
{
	var lstCustomer = new List<dynamic>();

	var customer = new Customer
	{
		Id = 1,
		Name = "Acme Inc",
		Email = "acme@email.com"
	};

	var customer1 = new Customer
	{
		Id = 2,
		Name = "Marvel Inc",
		Email = "Marvel@email.com"
	};

	lstCustomer.Add(customer);
	lstCustomer.Add(customer1);

	return lstCustomer;
}
```

## Usage with MemoryCache
Create the Attribute **[CacheableResult(600)]** 10 minutes Cache (600 seconds)

#### Code Sample
```
[CacheableResult(600)]
public List<Customer> ReturnCustomer()
{
	var lstCustomer = new List<Customer>();

	var customer = new Customer
	{
		Id = 1,
		Name = "Acme Inc",
		Email = "acme@email.com"
	};

	var customer1 = new Customer
	{
		Id = 2,
		Name = "Marvel Inc",
		Email = "Marvel@email.com"
	};

	lstCustomer.Add(customer);
	lstCustomer.Add(customer1);

	return lstCustomer;
}
```
	
## Clear / Invalidate Cache with Redis
Add **[RedisInvalidate("ReturnCustomer")** with the MethodName who Cached the information to invalidate/clear

#### Code Sample
```
[RedisInvalidate("ReturnCustomer")]
public bool UpdateCustomer()
{
	return true;
}
```

## Clear / Invalidate Cache with MemoryCache
Add **[AffectedCacheableMethods("ReturnCustomer")** with the MethodName who Cached the information to invalidate/clear

#### Code Sample
```
[RedisInvalidate("ReturnCustomer")]
public bool UpdateCustomer()
{
	return true;
}
```
