let api = "https://localhost:44389";
let courseArray;
let studentArray;
let currentStudent="";
init();
populateCourses();


//why is courseArray returning null.
//TODO: Change get student to populate universal array.

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
    
        document.getElementById("accordionStudents").innerHTML = "";
        var data = studentArray;

        for(let i=0; i<data.length; i++){
            var courselist ="";
            
            for(let a=0; a<data[i].courses.length; a++){
                courselist += `</br>     ID: ${data[i].courses[a].id} Categorey: ${data[i].courses[a].categorey} Name: ${data[i].courses[a].name}`
            };
           
            var courseSelectionMenu = populateCourseSelect(data[i].id);
            
            document.getElementById("accordionStudents").innerHTML += 
            `<div class="accordion-item">
                <h2 class="accordion-header" id="heading${i}">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${i}" aria-expanded="true" aria-controls="collapse${i}">
                    ${data[i].name}
                    </button>
                </h2>
                <div id="collapse${i}" class="accordion-collapse collapse" aria-labelledby="heading${i}" data-bs-parent="#accordionStudents">
                
                <div class="accordion-body">
                    
                <strong>ID:</strong> ${data[i].id} </br> 
                <strong>Name:</strong>  ${data[i].name}</br>
                <strong>Age:</strong> ${data[i].age} </br></br>
                <strong>Courses:</strong> ${courselist} </br>
                
                <button type="button" class="btn btn-info" style="margin-top:20px" onclick="selectStudent(${data[i].id})" data-bs-toggle="offcanvas" data-bs-target="#offcanvasExample${i}" aria-controls="offcanvasExample">Enroll in a Class</button> <button type="button" class="btn btn-secondary" style="margin-top:20px" >Drop a Class</button> <button type="button" class="btn btn-danger" style="margin-top:20px" onclick="removeStudent()">Remove Student</button>
                
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
                
                </div>
                </div>
            </div>`
        };
}
function populateCourses(){
    fetch(`${api}/student/courses`)
    .then((response) => (response.json()))
    .then((data) =>{
        console.log(data);
        document.getElementById("accordionCourses").innerHTML = "";
        for(let i=0; i<data.length; i++){
           
            document.getElementById("accordionCourses").innerHTML += 
            `<div class="accordion-item">
                <h2 class="accordion-header" id="heading${i}">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${i}" aria-expanded="true" aria-controls="collapse${i}">
                    ${data[i].name}
                    </button>
                </h2>
                <div id="collapse${i}" class="accordion-collapse collapse" aria-labelledby="heading${i}" data-bs-parent="#accordionCourses">
                
                <div class="accordion-body">
                   
                    <strong>ID:</strong>         ${data[i].id} </br> 
                    <strong>Name:</strong>      ${data[i].name}</br>
                    <strong>Categorey:</strong> ${data[i].categorey} </br>
                    <strong>Description:</strong> ${data[i].description} </br>
                    
                    
                </div>
            </div>
          </div>`
        };  
    })
}
function selectStudent(selectedStudent){
    currentStudent = selectedStudent;
}
function populateCourseSelect(tempStudentId){

    var courseDropDown = `<select class="form-select" aria-label="Default select example" style="margin-top:30px" id="courseDropDown${tempStudentId}">`;
    
    for(let i=0; i<courseArray.length; i++){
            courseDropDown += `<option value="${courseArray[i].id}">${courseArray[i].name}</option>`;
        }
        courseDropDown += `</select>`;
    return courseDropDown;
}
function finalizeEnrollment(tempStudentId){

   
    currentCourseSelection= document.getElementById(`courseDropDown${tempStudentId}`).value;

    var enrollmentSelection = {
        courseId: currentCourseSelection,
        studentId: currentStudent
    }

    fetch(`${api}/student/courseenroll`, {
        method: 'POST',
        body: JSON.stringify(enrollmentSelection),
        headers: {
            "Content-Type": "application/json"
        }
    })
}
