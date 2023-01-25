let api = "https://localhost:44389";
let courseArray;
let studentArray;
let currentStudentId="";
let currentCourseId="";
init();

//WISHLIST:

//TODO: FIX ENROLL A COURSE SO THAT YOU CAN'T ENROLL IN IN THE SAME COURSE TWICE---HAVE DROPDOWN POPULATE BASED AROUND UNENROLLED COURSES.
//TODO: IMPLEMENT A "VIEW ENROLLEES" BUTTON THAT CALLS A MODAL THAT SHOWS WHO IS ENROLLED IN THE COURSE.
//TODO: ADD CONFIRMATION MODALS FOR ENROLLING IN A CLASS, DROPPING A CLASS, REMOVING A STUDENT, CREATING A COURSE, EDITING A COURSE, AND DELETING A COURSE
//TODO: CLEANUP ENUM PARSING ON THE FRONT END (PARSE TO STRING BEFORE SENDING TO FRONTEND FROM BACKEND OR POPULATE A PARSE CATEGOREYENUM_INDEX TO CATEGOREYENUM_STRING ON FRONT END)
//TODO: ADD FORM VALIDATION TO CREATECOURSE() AND EDITCOURSE() FUNCTIONS.
//Efficiency refactor: refactor renderStudentPage() to utilize only one "offcanvas" that populated info based on current selection?

async function init(){
    const courses = await getCourses();
    const students = await getStudents();

    courseArray = courses;
    studentArray = students;

    renderStudentPage();
    
}
async function getCourses(){
    const response = await fetch(`${api}/student/courses`);
    const data = await response.json();
    return data;
}
async function getStudents(){
    const response = await fetch(`${api}/student`);
    const data = await response.json();
    return data;
    
}
function renderStudentPage(){
    
        document.getElementById("accordionDisplay").innerHTML = "";
        var data = studentArray;

        for(let i=0; i<data.length; i++){
            var courselist ="";
            
            for(let a=0; a<data[i].courses.length; a++){
                courselist += `</br>     ID: ${data[i].courses[a].id} Categorey: ${data[i].courses[a].categorey} Name: ${data[i].courses[a].name}`
            };
           
            var courseSelectionMenu = populateCourseSelect(data[i].id);
            var courseDropSelectMenu = populateCourseDropSelect(data[i]);
            
            document.getElementById("accordionDisplay").innerHTML += 
            `<div class="accordion-item">
                <h2 class="accordion-header" id="heading${i}">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${i}" aria-expanded="true" aria-controls="collapse${i}">
                    <p><strong>ID:</strong> ${data[i].id} || <strong>     Name:</strong> ${data[i].name}</p>
                    </button>
                </h2>
                <div id="collapse${i}" class="accordion-collapse collapse" aria-labelledby="heading${i}" data-bs-parent="#accordionDisplay">
                
                <div class="accordion-body">
                    
                <strong>ID:</strong> ${data[i].id} </br>
                <strong>Name:</strong>  ${data[i].name}</br>
                <strong>Age:</strong> ${data[i].age} </br></br>
                <strong>Courses:</strong> ${courselist} </br>
                
                <button type="button" class="btn btn-primary" style="margin-top:20px" onclick="selectStudent(${data[i].id})" data-bs-toggle="offcanvas" data-bs-target="#offcanvasExample${i}" aria-controls="offcanvasExample">Enroll in a Class</button> 
                <button type="button" class="btn btn-secondary" style="margin-top:20px" onclick="selectStudent(${data[i].id})" data-bs-toggle="offcanvas" data-bs-target="#dropClassOffCanvas${i}" aria-controls="dropClassOffCanvas")>Drop a Class</button> 
                <button type="button" class="btn btn-danger" style="margin-top:20px" onclick="removeStudent(${data[i].id})">Remove Student</button>
                
                <div class="offcanvas offcanvas-start" tabindex="-1" id="offcanvasExample${i}" aria-labelledby="offcanvasExampleLabel">
                    <div class="offcanvas-header">
                    <h5 class="offcanvas-title" id="offcanvasExampleLabel">Course Selection</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                </div>
  
                <div class="offcanvas-body">
                <div>
                    Select the course you'd like to enroll ${data[i].name} in, then click the "Finalize Enrollment" button.
                </div>
                    ${courseSelectionMenu}
            
                    <button type="button" class="btn btn-success" style="margin-top:40px" onclick="finalizeEnrollment(${data[i].id})">Finalize Enrollment</button>
                    
                </div>
                </div>
                </div>
                
                        <div class="offcanvas offcanvas-start" tabindex="-1" id="dropClassOffCanvas${i}" aria-labelledby="dropClassOffCanvasLabel">
                        <div class="offcanvas-header">
                            <h5 class="offcanvas-title" id="dropClassOffCanvasLabel">Drop a class</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                        </div>
                        <div class="offcanvas-body">
                        <div>
                            Select a course you wish to drop, then click the "Finalize Drop" button.
                        </div>
                         <div>
                            ${courseDropSelectMenu}
                            <button type="button" class="btn btn-success" style="margin-top:40px" onclick="dropClass(${data[i].id})">Finalize Drop</button>
                    
                                </div>
                            </div>
                        </div>

                </div>
                </div>
            </div>`
        };
        document.getElementById("mainPageButtonHeaders").innerHTML= `
        <h1 class="display-6" style="margin-left: 10px; margin-top: 10px; ">Student Page</h1>
        <div class="btn-group" style="margin-top: 20px; margin-left: 15px;">
            <a class="btn btn-primary active" aria-current="page" onclick="renderStudentPage()">Students</a>
            <a class="btn btn-primary" onclick="renderCoursePage()">Courses</a>
        </div>
        <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#addStudentModal" style="margin-top: 20px; margin-left: 15px;">Add a Student</button>`
}
function renderCoursePage(){
    
        document.getElementById("accordionDisplay").innerHTML = "";
        var data = courseArray;
        
        for(let i=0; i<data.length; i++){
           
            document.getElementById("accordionDisplay").innerHTML += 
            `<div class="accordion-item">
                <h2 class="accordion-header" id="heading${i}">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${i}" aria-expanded="true" aria-controls="collapse${i}">
                    ${data[i].name}
                    </button>
                </h2>
                <div id="collapse${i}" class="accordion-collapse collapse" aria-labelledby="heading${i}" data-bs-parent="#accordionDisplay">
                
                <div class="accordion-body">
                   
                    <strong>ID:</strong>         ${data[i].id} </br> 
                    <strong>Name:</strong>      ${data[i].name}</br>
                    <strong>Categorey:</strong> ${data[i].categorey} </br>
                    <strong>Description:</strong> ${data[i].description} </br>
                    
                    <button type="button" class="btn btn-primary" disabled style="margin-top: 20px">View Enrollment List</button>
                    <button type="button" class="btn btn-secondary" data-bs-toggle="modal" data-bs-target="#editCourseModal" style="margin-top: 20px" onclick="selectCourse(${data[i].id})">Edit Course</button>
                    <button type="button" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#deleteCourseModal"style="margin-top: 20px" onclick="selectCourse(${data[i].id})">Remove Course</button>
                    

                </div>
            </div>
          </div>`
        };  

        document.getElementById("mainPageButtonHeaders").innerHTML= `
        <h1 class="display-6" style="margin-left: 10px; margin-top: 10px; ">Courses Page</h1>
        <div class="btn-group" style="margin-top: 20px; margin-left: 15px;">
            <a class="btn btn-primary" aria-current="page" onclick="renderStudentPage()">Students</a>
            <a class="btn btn-primary active" onclick="renderCoursePage()">Courses</a>
        </div>
        <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#createCourseModal" style="margin-top: 20px; margin-left: 15px;">Create a Course</button>`
        
        
}
function selectStudent(selectedStudent){
    currentStudentId = selectedStudent;
}
function selectCourse(selectedCourseId){
    currentCourseId = selectedCourseId;
}
function populateCourseSelect(tempStudentId){

    var courseDropDown = `<select class="form-select" aria-label="Default select example" style="margin-top:30px" id="courseDropDown${tempStudentId}">`;
    
    for(let i=0; i<courseArray.length; i++){
            courseDropDown += `<option value="${courseArray[i].id}">${courseArray[i].name}</option>`;
        }
        courseDropDown += `</select>`;
    return courseDropDown;
}
function populateCourseDropSelect(tempStudent){
    var courseDropDropDown = `<select class="form-select" aria-label="Default select example" style="margin-top:30px" id="courseDropDropDown${tempStudent.id}">`;
    
    for(let i=0; i<tempStudent.courses.length; i++){
            courseDropDropDown += `<option value="${tempStudent.courses[i].id}">${tempStudent.courses[i].name}</option>`;
        }
        courseDropDropDown += `</select>`;
    return courseDropDropDown;
}
function finalizeEnrollment(tempStudentId){

   
    var currentCourseSelection= Number(document.getElementById(`courseDropDown${tempStudentId}`).value);

    var enrollmentSelection = {
        courseId: currentCourseSelection,
        studentId: currentStudentId
    }

    fetch(`${api}/student/courseenroll`, {
        method: 'POST',
        body: JSON.stringify(enrollmentSelection),
        headers: {
            "Content-Type": "application/json"
        }
    })
    .then((response) => response.json())
    .then((data) => {

    })
}
function dropClass(tempStudentId){
    var currentCourseSelection= Number(document.getElementById(`courseDropDropDown${tempStudentId}`).value);

    var dropSelection = {
        courseId: currentCourseSelection,
        studentId: currentStudentId
    }

    fetch(`${api}/student/coursedrop`, {
        method: 'POST',
        body: JSON.stringify(dropSelection),
        headers: {
            "Content-Type": "application/json"
        }
    })
    .then((response) => response.json())
    .then((data) => {

    })
}
function removeStudent(tempStudentId){

   var id = Number(tempStudentId);

    fetch(`${api}/student/removestudent`, {
        method: 'DELETE',
        body: JSON.stringify(id),
        headers: {
            "Content-Type": "application/json"
        }
    })
    .then((response) => response.json())
    .then((data) => {

    })
}
function addStudent(){

    var studentName = document.getElementById("floatingInputStudentName").value;
    var studentAge = Number(document.getElementById("floatingInputStudentAge").value);

    var isValid = validateAddStudentInput(studentName, studentAge)

    
    if(isValid){
        
        var newStudentInput = {
            name: studentName,
            age: studentAge
        }
        
        fetch(`${api}/student/addstudent`, {
            method: 'POST',
            body: JSON.stringify(newStudentInput),
            headers: {
                "content-type": "application/json"
            }
        })
        .then((response) => response.json())
        .then((data) => {
                console.log(data);
                var message = data.message;
                if(data.success=true){
                    alert(message);
                    init();
                }
                else{
                    alert(message);
                }
        })
    }
}
function validateAddStudentInput(studentName, studentAge){

    var validInput=true;

    if(studentName==""){
        document.getElementById("floatingInputStudentName").setAttribute("class","form-control is-invalid")
        document.getElementById("invalidStudentName").innerHTML = "Please enter a name."

        validInput = false;
    }
    if(studentAge ==0 || isNaN(studentAge) ==true){
        document.getElementById("floatingInputStudentAge").setAttribute("class","form-control is-invalid")
        document.getElementById("invalidStudentAge").innerHTML = "Please enter an age."

        validInput = false;
    }

    return validInput
}
function createCourse(){
    
    var courseName = document.getElementById("floatingInputCreateCourseName").value;
    var courseCategorey = Number(document.getElementById("floatingSelectCreateCourseCategorey").value);
    var courseDescription = document.getElementById("floatingTextareaCreateCourseDescription").value;

    var newCourseInput = {
        name: courseName,
        categorey: courseCategorey,
        description: courseDescription
    }
    
    fetch(`${api}/student/addcourse`, {
        method: 'POST',
        body: JSON.stringify(newCourseInput),
        headers: {
            "content-type": "application/json"
        }
    })
    .then((response) => response.json())
    .then((data) => {
            console.log(data);
    })
}
function finalizeCourseEdit(){

    var courseName = document.getElementById("floatingInputEditCourseName").value;
    var courseCategorey = Number(document.getElementById("floatingSelectEditCourseCategorey").value);
    var courseDescription = document.getElementById("floatingTextareaEditCourseDescription").value;

    var editCourseInput = {
        id: Number(currentCourseId),
        name: courseName,
        categorey: courseCategorey,
        description: courseDescription
    }
    
    fetch(`${api}/student/editcourse`, {
        method: 'POST',
        body: JSON.stringify(editCourseInput),
        headers: {
            "content-type": "application/json"
        }
    })
    .then((response) => response.json())
    .then((data) => {
            console.log(data);
    })
}
function finalizeCourseDelete(){
    
    var id = Number(currentCourseId);

    fetch(`${api}/student/removecourse`, {
        method: 'DELETE',
        body: JSON.stringify(id),
        headers: {
            "Content-Type": "application/json"
        }
    })
    .then((response) => response.json())
    .then((data) => {

    })
}