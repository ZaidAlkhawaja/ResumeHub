document.addEventListener('DOMContentLoaded', function () {
    // Section navigation
    document.querySelectorAll('[data-section]').forEach(link => {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            const sectionId = this.getAttribute('data-section');

            // Update active nav link
            document.querySelectorAll('.nav-pills .nav-link').forEach(navLink => {
                navLink.classList.remove('active');
            });
            this.classList.add('active');

            // Show corresponding section
            document.querySelectorAll('.form-section').forEach(section => {
                section.classList.remove('active');
            });
            document.getElementById(`${sectionId}-section`).classList.add('active');
        });
    });

    // Profile image preview
    document.getElementById('PersonalImage')?.addEventListener('change', function (e) {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (event) {
                document.getElementById('profileImagePreview').src = event.target.result;
            };
            reader.readAsDataURL(file);
        }
    });

    // Add Service
    document.getElementById('addService')?.addEventListener('click', function () {
        const container = document.getElementById('servicesContainer');
        const idx = container.querySelectorAll('.dynamic-list-item').length;
        const html = `
            <div class="dynamic-list-item">
                <i class="fas fa-times remove-item" onclick="removeItem(this)"></i>
                <div class="row g-3">
                    <div class="col-md-6">
                        <label class="form-label">Service Name*</label>
                        <input name="Services[${idx}].ServiceName" class="form-control" required />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Service Description</label>
                        <input name="Services[${idx}].ServiceDescription" class="form-control" />
                    </div>
                </div>
            </div>
        `;
        container.insertAdjacentHTML('beforeend', html);
    });

    // Add Project
    document.getElementById('addProject')?.addEventListener('click', function () {
        const container = document.getElementById('projectsContainer');
        const idx = container.querySelectorAll('.dynamic-list-item').length;
        const html = `
            <div class="dynamic-list-item">
                <i class="fas fa-times remove-item" onclick="removeItem(this)"></i>
                <div class="row g-3">
                    <div class="col-md-6">
                        <label class="form-label">Project Name*</label>
                        <input name="Projects[${idx}].ProjectName" class="form-control" required />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Project Link</label>
                        <input name="Projects[${idx}].ProjectLink" class="form-control" />
                    </div>
                    <div class="col-12">
                        <label class="form-label">Project Description*</label>
                        <textarea name="Projects[${idx}].ProjectDescription" class="form-control" rows="3" required></textarea>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Start Date*</label>
                        <input name="Projects[${idx}].StartDate" type="date" class="form-control" required />
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">End Date</label>
                        <input name="Projects[${idx}].EndDate" type="date" class="form-control" />
                    </div>
                    <div class="col-12">
                        <label class="form-label">Project Image</label>
                        <div class="image-preview-container">
                            <img src="https://via.placeholder.com/800x400" class="project-image-preview" alt="Project Preview" />
                            <div class="flex-grow-1">
                                <input name="Projects[${idx}].ProjectImage" type="file" class="form-control" accept="image/*" />
                                <div class="form-text">JPG or PNG, max 2MB</div>
                            </div>
                        </div>
                    </div>
                    <input type="hidden" name="Projects[${idx}].ImageBase64" />
                    <input type="hidden" name="Projects[${idx}].ImageFileName" />
                    <input type="hidden" name="Projects[${idx}].ImageContentType" />
                </div>
            </div>
        `;
        container.insertAdjacentHTML('beforeend', html);

        // Add image preview for the new project image input
        container.lastElementChild.querySelector('input[type="file"]').addEventListener('change', function (e) {
            const file = e.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (event) {
                    container.lastElementChild.querySelector('.project-image-preview').src = event.target.result;
                };
                reader.readAsDataURL(file);
            }
        });
    });

    // Add Skill
    document.getElementById('addSkill')?.addEventListener('click', function () {
        const container = document.getElementById('skillsContainer');
        const idx = container.querySelectorAll('.dynamic-list-item').length;
        const html = `
            <div class="dynamic-list-item">
                <i class="fas fa-times remove-item" onclick="removeItem(this)"></i>
                <div class="row g-3">
                    <div class="col-md-6">
                        <label class="form-label">Skill Name*</label>
                        <input name="Skills[${idx}].SkillName" class="form-control" required />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Skill Type*</label>
                        <select name="Skills[${idx}].SkillType" class="form-select" required>
                            <option value="">Select type</option>
                            <option>Programming Language</option>
                            <option>Framework</option>
                            <option>Tool</option>
                            <option>Design</option>
                        </select>
                    </div>
                </div>
            </div>
        `;
        container.insertAdjacentHTML('beforeend', html);
    });
});

// Remove item with animation
function removeItem(element) {
    const item = element.closest('.dynamic-list-item');
    item.style.transform = 'scale(0.95)';
    item.style.opacity = '0';
    setTimeout(() => {
        item.remove();
        // Re-index remaining items
        updateIndexes();
    }, 200);
}

// Update indexes for model binding
function updateIndexes() {
    ['Services', 'Projects', 'Skills'].forEach(prefix => {
        const container = document.getElementById(`${prefix.toLowerCase()}Container`);
        if (container) {
            const items = container.querySelectorAll('.dynamic-list-item');
            items.forEach((item, idx) => {
                item.querySelectorAll('[name]').forEach(input => {
                    const name = input.getAttribute('name');
                    if (name) {
                        const newName = name.replace(new RegExp(`${prefix}\\[\\d+\\]`), `${prefix}[${idx}]`);
                        input.setAttribute('name', newName);
                    }
                });
            });
        }
    });
}