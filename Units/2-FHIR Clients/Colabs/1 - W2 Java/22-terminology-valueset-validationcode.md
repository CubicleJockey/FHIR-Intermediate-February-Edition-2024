# Terminology: ValueSet/$validate_code

![Recipe Icon](./images/recipe-icon.png)

## Recipe # J-13: Invoking ValueSet / $validate_code
```java
Parameters outParams = client.operation()  
.onInstance(new IdDt("ValueSet","|Valueset_Id|"))
.named("validate-code")  
.withParameter(Parameters.class, "system", new UriType("|code_system|"))
.andParameter("code",new StringType("|code|"))  
.execute();  
String result = outParams.getParameter().get(0).getValue().toString(); 
String message= outParams.getParameter().get(1).getValue().toString();  
String display=outParams.getParameter().get(2).getValue().toString(); 
System.out.println(result+":"+message+":"+display);  
```
You need to replace:

- |valueset_id|: Id for the valueset
- |code|: Code needing validation
- |code_system|: Code system for the code

![Micro-Assessment](./images/micro-assignment-icon.png)

## Micro Assignment #J-13: ValueSet Code Validation
Validate the code 8867-4 for the Value Set LG33055-1 (Group for Vitals -> Heart Rate) at https://fhir.loinc.org (note: this LOINC server by Regenstrief requires BasicAuth, and you need to open a free account).

The response should be: BooleanType[true]:Validation succeeded:Heart rate.
