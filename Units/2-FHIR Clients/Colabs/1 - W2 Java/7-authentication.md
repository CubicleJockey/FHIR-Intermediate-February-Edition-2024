# Authentication
All authentication options in HAPI FHIR are implemented using interceptors.

## Authentication with Bearer Token
- **Import the required interceptor classes.**
  ```java
  import ca.uhn.fhir.rest.client.api.IclientInterceptor;  
  import ca.uhn.fhir.rest.client.interceptor.BearerTokenAuthInterceptor;
  ```
- **Parameterize the Bearer token interceptor with your token.**
  *Note: You will need to obtain the token from an authorization server.*
  ```java
  String token = "ya29.QQIBibTwvKkE39hY8mdkT_mXZoRh7Ub9cK9hNsqrxem4QJ6sQa36VHfyuBe";  
  IClientInterceptor authInterceptor = new BearerTokenAuthInterceptor(token);
  ```
- **Register the interceptor in your client before attempting any operation.**
  ```java
  Client.registerInterceptor(authInterceptor);
  ```

## Basic Authentication
Remember that a basic authentication server requires sending your user name and password. Basic authentication is not recommended, but if you need to implement it, this is how it's done.
1. **Import the required interceptor classes**
  ```java
  import ca.uhn.fhir.rest.client.api.IclientInterceptor;  
  import ca.uhn.fhir.rest.client.interceptor.BasicAuthInterceptor;
  ```
2. **Parameterize the basic interceptor with your user and password**
  ```java
  String uname = "your_user_name";  
  String pword = "your_password";  
  IClientInterceptor authInterceptor = new BasicAuthInterceptor(uname, pword);
  ```
3. **Register the interceptor in your client before attempting any operation**
  ```java
  Client.registerInterceptor(authInterceptor);
  ```
![Micro-Assignment Icon](./images/micro-assignment-icon.png)

## Micro Assignment #J-2: Try different authentication methods
- Modify the MA_J01_Skeleton to use Basic Authentication.
- Modify the MA_J01_Skeleton to use Bearer Token authentication.
