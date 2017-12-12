# TestaCache
Testa Cache Component to Create Cache using MemoryCache or Redis in C#

## How to Use:
Open Package Manager Console and run:
```Install-Package TestaCache```

## .NET Framework Support
.NET Framework 4.6+
.NET Core 1.1/2.0 will be supported in v1.1 (under development)

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
Create the Attribute **[CacheableResult]**

#### Code Sample
```
[CacheableResult]
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
