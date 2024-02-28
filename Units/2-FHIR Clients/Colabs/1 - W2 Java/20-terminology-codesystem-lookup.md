## Terminology: CodeSystem / $lookup
In this template we are only showing the first return parameter, but depending on the specific terminology, by going over all the parameters, you can extract information about hierarchies, properties, etc.

![Recipe Icon](./images/recipe-icon.png)

## Recipe # J-11: Invoking CodeSystem / $lookup
```java
try {    
  Parameters outParams = client  
   .operation()  
   .onType(CodeSystem.class)  
   .named("lookup")  
   .withParameter(Parameters.class, "code",  
    new CodeType("|code|"))  
   .andParameter("system",   
   new UriType("|code_system|"))  
   .execute();  
  String displayTitle = outParams.getParameter()
     .get(0).getName().toString();  
  String displayValue = outParams.getParameter()
     .get(0).getValue().primitiveValue();  
  System.out.println(displayTitle+":"+displayValue);  
}  
catch (Exception ex)  
{  
   System.out.println("Error:" +e.getMessage());  
}  
```
You need to replace:

- |code|: code to lookup
- |code_system|: code_system for the code

![Micro-Assessment](./images/micro-assignment-icon.png)

## Micro Assignment #J-11: Validate using the lookup method
Use the lookup method to validate the code 73211009 for SNOMED CT.
