﻿     // Search functionality
    document.querySelector('.search-box input').addEventListener('input', function(e) {
            const searchTerm = e.target.value.toLowerCase();
    const resumes = document.querySelectorAll('.resume-card');
    let hasResults = false;
            
            resumes.forEach(resume => {
                const title = resume.querySelector('.resume-title').textContent.toLowerCase();
    if (title.includes(searchTerm)) {
        resume.style.display = 'block';
    hasResults = true;
                } else {
        resume.style.display = 'none';
                }
            });

    // Show empty state if no results
    const emptyState = document.querySelector('.empty-state');

    if (!hasResults && searchTerm !== '') {
        emptyState.style.display = 'block';
    emptyState.querySelector('h4').textContent = 'No matching resumes found';
    emptyState.querySelector('p').textContent = 'Try a different search term';
            } else {
        emptyState.style.display = 'none';
            }
        });
