# Headers for Encoding
In HAPI FHIR, this is a client parameter, allowing you to choose which syntax you expect to send and/or receive from the server:

![Recipe Icon](./images/example-icon.png)

## Recipe #J-1: Setting the Resource Encoding (JSON or XML)
```java
client.setEncoding(EncodingEnum.|encoding|);   
```
You need to replace `|encoding|` with either `JSON` or `XML`.

## Other headers
HAPI FHIR allows the use of cookies through a cookie interceptor.

![Micro-Assignment Icon](./images/micro-assignment-icon.png)

## Micro Assignment #J-3: Change HAPI FHIR client Encoding
- Modify the MA_J01_Skeleton to use JSON encoding.
