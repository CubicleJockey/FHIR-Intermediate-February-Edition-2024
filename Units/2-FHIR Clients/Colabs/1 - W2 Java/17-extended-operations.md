![Micro-Assessment](./images/micro-assignment-icon.png)

## Micro Assignment #J-9: Use $everything to obtain all resources for a patient
Obtain all resources associated with patient 72191 from December 2019 to March 2020.

![Recipe Icon](./images/recipe-icon.png)

## Recipe # J-10: Executing an extended operation on a specific resource instance
To specify an operation for a specific instance, you need to add the `onInstance` method specifying the resource and id.

```java
org.hl7.fhir.r4.model.|ParameterType| |ParameterVarName| =   
new |ParameterType|("|ParameterValue|");  
Parameters inParams = new Parameters();  
{  
     inParams.addParameter().setName("|ParameterName|")  
   .setValue(ParameterVarName);  
    ...  
}  
Parameters outParams = client.operation()  
.onInstance(new IdDt("|ResourceClass|", "|ResourceId|"))
.named("$|OperationName|")  
.withParameters(inParams)
```
You need to replace:

- |ResourceClass|  
The name of the resource class. Example: Patient

- |ResourceId|  
The resource id for the resource instance you need to delete

- |ParameterType|  
Parameter type defined for the operation (string,date,etc)

- |ParameterVarName|  
Name of the variable holding the parameter value

- |ParameterName|  
Name of the parameter

- |ParameterValue|  
Value of the parameter

- |OperationName|  
Name of the operation (example: $everything)

![Example Icon](./images/example-icon.png)

## Example #J-8: Executing the $everything extended operation
Executing the `$everything` method for a Patient instance for a given period.

```java
org.hl7.fhir.r4.model.DateType dtBeg = new DateType("2019-11-01");  
org.hl7.fhir.r4.model.DateType dtEnd = new DateType("2020-02-02");  
Parameters inParams = new Parameters();  
{  
  inParams.addParameter().setName("start").setValue(dtBeg);  
  inParams.addParameter().setName("end").setValue(dtEnd);  
}  
  
Parameters outParams = client  
.operation()  
.onInstance(new IdDt("Patient", "73412"))  
.named("$everything")  
.withParameters(inParams) 
```
