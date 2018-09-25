function validateForm(event) {
    //get data input into the fields:
    var name = document.forms['main-contact-form']['name'].value;
    var email = document.forms['main-contact-form']['email'].value;
    var subject = document.forms['main-contact-form']['subject'].value;
    var message = document.forms['main-contact-form']['message'].value;

    //output to the spans:
    var nameVal = document.getElementById('nameVal');
    var emailVal = document.getElementById('emailVal');
    var subjectVal = document.getElementById('subjectVal');
    var messageVal = document.getElementById('messageVal');
    var emailRegEx = new RegExp(/^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/);
    //test all conditions before sending the data:
    if (name.length == 0 || email.length == 0 || subject.length == 0 || message.length == 0|| !emailRegEx.test(email)) {
        if (name.length == 0) {
            nameVal.textContent = "* Name is required";
        }
        else {
            nameVal.textContent = ""; //this is just to allow us to add something later if we want
        }
        if (email.length == 0) {
            emailVal.textContent = "* Email is required";
        }
        else {
            emailVal.textContent = ""; //this is just to allow us to add something later if we want
        }
        if (subject.length == 0) {
            subjectVal.textContent = "* Subject is required";
        }
        else {
            subjectVal.textContent = ""; //this is just to allow us to add something later if we want
        }
        if (message.length == 0) {
            messageVal.textContent = "* Message is required";
        }
        else {
            messageVal.textContent = ""; //this is just to allow us to add something later if we want
        }
        if (!emailRegEx.test(email)&& email.length >0) {
            emailVal.textContent = "*Please enter a valid email address";
        }
        if (emailRegEx.test(email) && email.length > 0) {
            emailVal.textContent = "";
        }
        //Prevent the submit event from occurring
        event.preventDefault();
    }
}