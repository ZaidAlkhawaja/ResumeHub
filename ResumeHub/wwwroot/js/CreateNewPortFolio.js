//let currentStep = 0;
//const steps = document.querySelectorAll('.step');
//const totalSteps = steps.length;

//function showStep(index) {
//    steps.forEach((step, i) => {
//        step.classList.toggle('active', i === index);
//    });
//    // Update buttons visibility
//    document.getElementById('prevBtn').style.display = index === 0 ? 'none' : 'inline-block';
//    document.getElementById('nextBtn').style.display = index === totalSteps - 1 ? 'none' : 'inline-block';
//    document.getElementById('previewBtn').style.display = index === totalSteps - 1 ? 'inline-block' : 'none';
//    document.getElementById('enhanceBtn').style.display = 'none';
//    // Reset preview if navigating back
//    if (index < totalSteps - 1) {
//        document.getElementById('previewSection').style.display = 'none';
//    }
//    // Update progress bar length
//    const progressPercent = (index / (totalSteps - 1)) * 100;
//    document.getElementById('progressBar').style.width = progressPercent + '%';
//}

//function nextStep() {
//    if (currentStep < totalSteps - 1) {
//        currentStep++;
//        showStep(currentStep);
//    }
//}

//function prevStep() {
//    if (currentStep > 0) {
//        currentStep--;
//        showStep(currentStep);
//    }
//}

//function showPreview() {
//    // Collect inputs
//    const firstName = document.getElementById('firstName').value.trim();
//    const lastName = document.getElementById('lastName').value.trim();
//    const phoneNumber = document.getElementById('phoneNumber').value.trim();
//    const email = document.getElementById('emailAddress').value.trim();
//    const github = document.getElementById('githubLink').value.trim();
//    const linkedin = document.getElementById('linkedinLink').value.trim();
//    const slack = document.getElementById('slackLink').value.trim();
//    const summary = document.getElementById('summary').value.trim();
//    const servicesProjects = document.getElementById('servicesProjects').value.trim();
//    const skills = document.getElementById('skills').value.trim();

//    // Build preview content
//    const personalText = `
//                Name: ${firstName || '[Not provided]'} ${lastName || ''}<br>
//                Phone: ${phoneNumber || '[Not provided]'}<br>
//                Email: ${email || '[Not provided]'}<br>
//                GitHub: ${github || '[Not provided]'}<br>
//                LinkedIn: ${linkedin || '[Not provided]'}<br>
//                Slack: ${slack || '[Not provided]'}<br>
//                Summary: ${summary || '[Not provided]'}
//            `;
//    document.getElementById('previewPersonal').innerHTML = personalText;
//    document.getElementById('previewServices').textContent = servicesProjects || '[No services/projects provided]';
//    document.getElementById('previewSkills').textContent = skills || '[No skills provided]';

//    // Reveal preview section & buttons
//    document.getElementById('previewSection').style.display = 'block';
//    document.getElementById('previewBtn').style.display = 'none';
//    document.getElementById('enhanceBtn').style.display = 'inline-block';
//}

//function sendToAI() {
//    // Construct payload
//    const payload = {
//        firstName: document.getElementById('firstName').value.trim(),
//        lastName: document.getElementById('lastName').value.trim(),
//        phoneNumber: document.getElementById('phoneNumber').value.trim(),
//        emailAddress: document.getElementById('emailAddress').value.trim(),
//        githubLink: document.getElementById('githubLink').value.trim(),
//        linkedinLink: document.getElementById('linkedinLink').value.trim(),
//        slackLink: document.getElementById('slackLink').value.trim(),
//        summary: document.getElementById('summary').value.trim(),
//        servicesProjects: document.getElementById('servicesProjects').value.trim(),
//        skills: document.getElementById('skills').value.trim()
//    };

//    // Send data to AI endpoint
//    fetch('/api/portfolio/enhance', {
//        method: 'POST',
//        headers: {
//            'Content-Type': 'application/json'
//        },
//        body: JSON.stringify(payload)
//    })
//        .then(response => response.json())
//        .then(data => {
//            console.log('AI response:', data);
//            alert('Portfolio content generated successfully!');
//        })
//        .catch(err => {
//            console.error('Error sending to AI:', err);
//            alert('There was an error generating your portfolio. Please try again.');
//        });
//}

//document.addEventListener('DOMContentLoaded', () => {
//    showStep(currentStep);
//    document.getElementById('portfolioForm').scrollIntoView({ behavior: 'smooth' });
//});



// ~/wwwroot/js/NewPortfolio.js

// Re-index all project items so that their name/data-valmsg-for attributes remain sequential

function reindexProjects() {

    const container = document.getElementById('projectsContainer');

    const items = container.querySelectorAll('.project-item');

    items.forEach((item, index) => {

        // Grab each control inside this .project-item

        const nameInput = item.querySelector('.project-name');

        const descTextarea = item.querySelector('.project-description');

        const linkInput = item.querySelector('.project-link');

        const fileInput = item.querySelector('.project-image');

        // Rename them to Projects[index].*

        nameInput.name = `Projects[${index}].Name`;

        descTextarea.name = `Projects[${index}].Description`;

        linkInput.name = `Projects[${index}].Link`;

        fileInput.name = `Projects[${index}].Image`;

        // Update validation spans

        const nameSpan = item.querySelector('[data-valmsg-for*="Name"]');

        const descSpan = item.querySelector('[data-valmsg-for*="Description"]');

        const linkSpan = item.querySelector('[data-valmsg-for*="Link"]');

        const fileSpan = item.querySelector('[data-valmsg-for*="Image"]');

        if (nameSpan) nameSpan.setAttribute('data-valmsg-for', `Projects[${index}].Name`);

        if (descSpan) descSpan.setAttribute('data-valmsg-for', `Projects[${index}].Description`);

        if (linkSpan) linkSpan.setAttribute('data-valmsg-for', `Projects[${index}].Link`);

        if (fileSpan) fileSpan.setAttribute('data-valmsg-for', `Projects[${index}].Image`);

    });

}

document.addEventListener('DOMContentLoaded', () => {

    const addProjectBtn = document.getElementById('addProjectBtn');

    const projectsContainer = document.getElementById('projectsContainer');

    // Add a new project block

    addProjectBtn.addEventListener('click', () => {

        const projectCount = projectsContainer.querySelectorAll('.project-item').length;

        const newHtml = `
<div class="project-item mb-4 p-3 border rounded">
<div class="mb-3">
<label class="form-label">Project Name*</label>
<input name="Projects[${projectCount}].Name"

                           class="form-control project-name"

                           required />
<span class="text-danger field-validation-valid"

                          data-valmsg-for="Projects[${projectCount}].Name"

                          data-valmsg-replace="true"></span>
</div>
<div class="mb-3">
<label class="form-label">Description*</label>
<textarea name="Projects[${projectCount}].Description"

                              class="form-control project-description"

                              rows="3"

                              required></textarea>
<span class="text-danger field-validation-valid"

                          data-valmsg-for="Projects[${projectCount}].Description"

                          data-valmsg-replace="true"></span>
</div>
<div class="mb-3">
<label class="form-label">Project Link</label>
<input name="Projects[${projectCount}].Link"

                           class="form-control project-link"

                           type="url" />
<span class="text-danger field-validation-valid"

                          data-valmsg-for="Projects[${projectCount}].Link"

                          data-valmsg-replace="true"></span>
</div>
<div class="mb-3">
<label class="form-label">Project Image</label>
<input name="Projects[${projectCount}].Image"

                           type="file"

                           class="form-control project-image"

                           accept="image/*" />
<img class="project-preview img-preview mt-2"

                         style="display:none;"

                         alt="Project Preview" />
<span class="text-danger field-validation-valid"

                          data-valmsg-for="Projects[${projectCount}].Image"

                          data-valmsg-replace="true"></span>
</div>
<button type="button" class="btn btn-danger btn-sm remove-project">Remove Project</button>
</div>

        `;

        projectsContainer.insertAdjacentHTML('beforeend', newHtml);

        // Attach “remove” handler to the newly inserted block

        const lastItem = projectsContainer.lastElementChild;

        lastItem.querySelector('.remove-project').addEventListener('click', () => {

            lastItem.remove();

            reindexProjects();

        });

    });

    // If there are existing project blocks on load, wire up their remove buttons

    projectsContainer.querySelectorAll('.remove-project').forEach(btn => {

        btn.addEventListener('click', function () {

            this.closest('.project-item').remove();

            reindexProjects();

        });

    });

});

// Add to ~/wwwroot/js/NewPortfolio.js (at the end of the file)

// Handle profile image preview and base64 conversion

const profileImageInput = document.querySelector('input[asp-for="ProfileImage"]');

if (profileImageInput) {

    profileImageInput.addEventListener('change', function (e) {

        const file = e.target.files[0];

        if (file) {

            const reader = new FileReader();

            reader.onload = function (event) {

                const preview = document.getElementById('profilePreview');

                preview.src = event.target.result;

                preview.style.display = 'block';

                // Store base64 in a hidden field

                document.querySelector('input[name="ProfileImageBase64"]').value = event.target.result;

            };

            reader.readAsDataURL(file);

        }

    });

}

// Handle project image previews and base64 conversion

document.addEventListener('change', function (e) {

    if (e.target && e.target.matches('.project-image')) {

        const file = e.target.files[0];

        if (file) {

            const reader = new FileReader();

            reader.onload = function (event) {

                const preview = e.target.closest('.project-item').querySelector('.project-preview');

                preview.src = event.target.result;

                preview.style.display = 'block';

                // Store base64 in a hidden field

                const index = Array.from(document.querySelectorAll('.project-image')).indexOf(e.target);

                document.querySelector(`input[name="Projects[${index}].ImageBase64"]`).value = event.target.result;

            };

            reader.readAsDataURL(file);

        }

    }

});

// Initialize previews if base64 data exists on page load

document.addEventListener('DOMContentLoaded', function () {

    // Profile image

    const profileBase64 = document.querySelector('input[name="ProfileImageBase64"]')?.value;

    if (profileBase64) {

        const preview = document.getElementById('profilePreview');

        preview.src = profileBase64;

        preview.style.display = 'block';

    }

    // Project images

    document.querySelectorAll('input[name$=".ImageBase64"]').forEach(input => {

        const base64 = input.value;

        if (base64) {

            const index = input.name.match(/\[(\d+)\]/)[1];

            const preview = document.querySelector(`.project-item:nth-child(${parseInt(index) + 1}) .project-preview`);

            if (preview) {

                preview.src = base64;

                preview.style.display = 'block';

            }

        }

    });

});
