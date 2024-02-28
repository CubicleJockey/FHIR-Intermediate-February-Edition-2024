# Reading resources

## Direct Read (READ)
In order to perform a direct read of a FHIR resource instance when you know the resource id in Java HAPI FHIR, you need to do this:

![Recipe Icon](./images/recipe-icon.png)

### Recipe #J-3: Direct Read
```java
|ResourceClass| |MyResourceName| = client.read()
.resource(|ResourceClass|.class).withId(s:"|ResourceId|").execute(); 
```
You need to replace:
- `|ResourceClass|` with the name of the resource class. Example: Patient
- `|ResourceId|` with the server-assigned logical id of the resource
- `|MyResourceName|` with the variable holding the instance after the call

![Example Icon](./images/example-icon.png)

#### Example #J-2: Read a patient instance from the logical id
This would read the patient with id=49293
```java
import org.hl7.fhir.r4.model.Patient  
...
Patient MyPatient = client.read()
.resource(Patient.class).withId(s:"49293").execute();  
```

## Version Read (VREAD)
In order to perform a version-aware read of a specific version of a FHIR resource instance when you know the resource id and version in Java HAPI FHIR, you need to do this:

![Recipe Icon](./images/recipe-icon.png)

### Recipe #J-4: Version Read
```java
|ResourceClass| |MyResourceName| = client.read()
.resource(|ResourceClass|.class)  
.withIdAndVersion(s:"|ResourceId|",s1:"|ResourceVersion|").execute();  
```
You need to replace:
- `|ResourceClass|` with the name of the resource class. Example: Patient
- `|ResourceId|` with the server-assigned logical id of the resource
- `|ResourceVersion|` with the version of the resource
- `|MyResourceName|` with the variable holding the instance after the call

![Example Icon](./images/example-icon.png)

#### Example #J-3: Read a Specific Version of a Resource Instance
This would read the patient with id=49293 and version 2
```java
import org.hl7.fhir.r4.model.Patient  
Patient MyPatient = client.read().resource(Patient.class).withId(s:"49293",s1:"2").execute();  
```

## URL Read
In order to perform a version-aware read of a specific version of a FHIR resource instance knowing the endpoint, resource id and version in Java HAPI FHIR, you need to do this:

![Recipe Icon](./images/recipe-icon.png)

### Recipe #J-5: URL Read
```java
|ResourceClass| |MyResourceName| = client.read()
.resource(|ResourceClass|.class)  
.withUrl (s:"|ResourceFullUrl|").execute();  
```
You need to replace:
- `|ResourceClass|` with the name of the resource class. Example: Patient
- `|ResourceFullUrl|` with the full address endpoint + assigned id/version of the resource
- `|MyResourceName|` with the variable holding the instance after the call

![Example Icon](./images/example-icon.png)

#### Example #J-4: Read a Resource by URL
This would read the patient with url= http://test.fhir.org/r4/Patient/example
```java
import org.hl7.fhir.r4.model.Patient  
...
Patient MyPatient = client.read()
.resource(Patient.class).withUrl(s:"http://test.fhir.org/r4/Patient/example")
.execute(); 
```

![Micro-Assignment](./images/micro-assignment-icon.png)

## Micro Assignment #J-4: Read using all methods
- Modify the MA_J01_Skeleton to read the patient resource with id 89293.
- Modify the MA_J01_Skeleton to v-read the patient resource with id 89293 / version 1.
- Modify the MA_J01_Skeleton to read the patient resource at URL: http://test.fhir.org/r4/Patient/example.
Note: The provided ids are just examples.

If in doubt, use Postman first to select a patient to read.
