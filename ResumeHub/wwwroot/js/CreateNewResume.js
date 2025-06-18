
    //document.addEventListener('DOMContentLoaded', function() {
    //        // Navigation between steps
    //        const formSections = document.querySelectorAll('.form-section');
    //const steps = document.querySelectorAll('.step');
    //const stepLines = document.querySelectorAll('.step-line');

    //        // Next button click handler
    //        document.querySelectorAll('.next-step').forEach(button => {
    //    button.addEventListener('click', async function () {
    //        const currentSection = document.querySelector('.form-section.active');
    //        const nextStepNum = this.getAttribute('data-next');
    //        const actionUrl = this.getAttribute('data-action');

    //        // Validate current section before proceeding
    //        if (validateSection(currentSection)) {
    //            // If there's an action URL, submit the form data to the server
    //            if (actionUrl) {
    //                const form = document.getElementById('resumeForm');
    //                const formData = new FormData(form);

    //                try {
    //                    const response = await fetch(actionUrl, {
    //                        method: 'POST',
    //                        body: formData,
    //                        headers: {
    //                            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
    //                        }
    //                    });

    //                    if (!response.ok) {
    //                        throw new Error('Network response was not ok');
    //                    }

    //                    const result = await response.json();

    //                    // Update the hidden resumeId if returned
    //                    if (result.id) {
    //                        document.getElementById('resumeId').value = result.id;
    //                    }

    //                    // Proceed to next step
    //                    currentSection.classList.remove('active');
    //                    document.querySelector(`.form-section[data-step="${nextStepNum}"]`).classList.add('active');
    //                    updateStepIndicator(nextStepNum);

    //                    // If going to preview step, update preview
    //                    if (nextStepNum === '3') {
    //                        updatePreview();
    //                    }
    //                } catch (error) {
    //                    console.error('Error:', error);
    //                    alert('An error occurred while saving your data. Please try again.');
    //                }
    //            } else {
    //                // No action URL, just proceed to next step
    //                currentSection.classList.remove('active');
    //                document.querySelector(`.form-section[data-step="${nextStepNum}"]`).classList.add('active');
    //                updateStepIndicator(nextStepNum);

    //                // If going to preview step, update preview
    //                if (nextStepNum === '3') {
    //                    updatePreview();
    //                }
    //            }
    //        }
    //    });
    //        });

    //        // Previous button click handler
    //        document.querySelectorAll('.prev-step').forEach(button => {
    //    button.addEventListener('click', function () {
    //        const currentSection = document.querySelector('.form-section.active');
    //        const prevStepNum = this.getAttribute('data-prev');

    //        currentSection.classList.remove('active');
    //        document.querySelector(`.form-section[data-step="${prevStepNum}"]`).classList.add('active');

    //        // Update step indicator
    //        updateStepIndicator(prevStepNum);
    //    });
    //        });

    //// Form submission for final step
    //document.getElementById('resumeForm').addEventListener('submit', async function(e) {
    //    e.preventDefault();

    //if (!document.getElementById('consentCheckbox').checked) {
    //    alert('Please agree to send your information to our AI for enhancement.');
    //return;
    //            }

    //const form = this;
    //const formData = new FormData(form);

    //try {
    //                const response = await fetch(form.getAttribute('action') || form.getAttribute('formaction'), {
    //    method: 'POST',
    //body: formData,
    //headers: {
    //    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
    //                    }
    //                });

    //if (!response.ok) {
    //                    throw new Error('Network response was not ok');
    //                }

    //const result = await response.json();

    //// Redirect or show success message
    //if (result.success) {
    //    window.location.href = result.redirectUrl || '/resume/success';
    //                } else {
    //    alert(result.message || 'An error occurred. Please try again.');
    //                }
    //            } catch (error) {
    //    console.error('Error:', error);
    //alert('An error occurred while submitting your resume. Please try again.');
    //            }
    //        });

    //// Function to validate a section
    //function validateSection(section) {
    //            const inputs = section.querySelectorAll('input[required], textarea[required]');
    //let isValid = true;

    //            inputs.forEach(input => {
    //                if (!input.value.trim()) {
    //    input.classList.add('is-invalid');
    //isValid = false;
    //                } else {
    //    input.classList.remove('is-invalid');
    //                }
    //            });

    //if (!isValid) {
    //                const firstInvalid = section.querySelector('.is-invalid');
    //firstInvalid.scrollIntoView({behavior: 'smooth', block: 'center' });
    //alert('Please fill in all required fields before proceeding.');
    //            }

    //return isValid;
    //        }

    //// Function to update step indicator
    //function updateStepIndicator(activeStep) {
    //    steps.forEach(step => {
    //        const stepNum = step.getAttribute('data-step');
    //        step.classList.remove('active', 'completed');

    //        if (stepNum === activeStep) {
    //            step.classList.add('active');
    //        } else if (stepNum < activeStep) {
    //            step.classList.add('completed');
    //        }
    //    });

    //            stepLines.forEach((line, index) => {
    //    line.classList.remove('completed');
    //if ((index + 1) < activeStep) {
    //    line.classList.add('completed');
    //                }
    //            });
    //        }

    //// Function to update preview
    //function updatePreview() {
    //            // Personal Information
    //            const personalPreview = `
    //<p><strong>Name:</strong> ${document.getElementById('firstName').value} ${document.getElementById('lastName').value}</p>
    //<p><strong>Title:</strong> ${document.getElementById('title').value}</p>
    //<p><strong>Email:</strong> ${document.getElementById('email').value}</p>
    //<p><strong>Phone:</strong> ${document.getElementById('phone').value}</p>
    //${document.getElementById('linkedin').value ? `<p><strong>LinkedIn:</strong> ${document.getElementById('linkedin').value}</p>` : ''}
    //${document.getElementById('github').value ? `<p><strong>GitHub:</strong> ${document.getElementById('github').value}</p>` : ''}
    //<p><strong>Summary:</strong> ${document.getElementById('summary').value}</p>
    //`;
    //document.getElementById('previewPersonal').innerHTML = personalPreview;

    //// Experience
    //const experienceText = document.getElementById('experience').value;
    //const experienceHTML = experienceText ?
    //`<div class="preview-section"><p>${experienceText.replace(/\n/g, '<br>')}</p></div>` :
    //'<p>No experience added</p>';
    //document.getElementById('previewExperience').innerHTML = experienceHTML;

    //// Education
    //const educationText = document.getElementById('education').value;
    //const educationHTML = educationText ?
    //`<div class="preview-section"><p>${educationText.replace(/\n/g, '<br>')}</p></div>` :
    //'<p>No education added</p>';
    //document.getElementById('previewEducation').innerHTML = educationHTML;

    //// Skills
    //const skillsText = document.getElementById('skills').value;
    //const skillsHTML = skillsText ?
    //`<div class="preview-section"><p>${skillsText.replace(/\n/g, '<br>')}</p></div>` :
    //'<p>No skills added</p>';
    //document.getElementById('previewSkills').innerHTML = skillsHTML;
    //        }
//    });


    //document.addEventListener('DOMContentLoaded', function() {
    //        // Client-side validation for required fields
    //        const form = document.getElementById('resumeForm');

    //form.addEventListener('submit', function(e) {
    //            const currentSection = document.querySelector('.form-section.active');
    //const requiredFields = currentSection.querySelectorAll('[required]');
    //let isValid = true;

    //            requiredFields.forEach(field => {
    //                if (!field.value.trim()) {
    //    field.classList.add('is-invalid');
    //isValid = false;
    //                } else {
    //    field.classList.remove('is-invalid');
    //                }
    //            });

    //if (!isValid) {
    //    e.preventDefault();
    //const firstInvalid = currentSection.querySelector('.is-invalid');
    //firstInvalid.scrollIntoView({behavior: 'smooth', block: 'center' });
    //alert('Please fill in all required fields before proceeding.');
    //            }
    //        });
//    });
// CreateNewResume.js
document.addEventListener('DOMContentLoaded', function () {
    // Handle form submission to prevent full page reload
    const form = document.getElementById('resumeForm');

    form.addEventListener('submit', function (e) {
        e.preventDefault();

        // Get the button that was clicked
        const submitter = e.submitter;
        const command = submitter ? submitter.value : '';

        // Update the current step hidden field
        const currentStepInput = document.querySelector('input[name="CurrentStep"]');
        let currentStep = parseInt(currentStepInput.value);

        if (command === 'next') {
            // Validate current step before proceeding
            if (!validateStep(currentStep)) {
                return;
            }
        }

        // Submit the form normally
        form.submit();
    });

    function validateStep(step) {
        let isValid = true;

        if (step === 1) {
            // Validate personal information
            if (!form.checkValidity()) {
                form.classList.add('was-validated');
                isValid = false;
            }
        }
        // Add validation for other steps if needed

        return isValid;
    }
});