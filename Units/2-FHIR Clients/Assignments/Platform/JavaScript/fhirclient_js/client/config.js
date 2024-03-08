const ServerEndpoint = ()=> {
    return "http://wildfhir4.aegis.net/fhir4-0-1";
}
const TerminologyServerEndpoint = () => {
    return "https://r4.ontoserver.csiro.au/fhir";
}
const AssignmentSubmissionFHIRServer = () => {
    return "http://fhirserver.hl7fundamentals.org/fhir/";
}
const StudentId= () => {
    return "kaminker.diego@gmail.com";
}
const StudentName=()=>{
    return "Diego Kaminker";
}
const PatientIdentifierSystem=()=>{
    return "http://fhirintermediate.org/patient_id";
}
exports.ServerEndpoint=ServerEndpoint;
exports.TerminologyServerEndpoint=TerminologyServerEndpoint;
exports.AssignmentSubmissionFHIRServer=AssignmentSubmissionFHIRServer;
exports.StudentId=StudentId;
exports.StudentName=StudentName;
exports.PatientIdentifierSystem=PatientIdentifierSystem;
