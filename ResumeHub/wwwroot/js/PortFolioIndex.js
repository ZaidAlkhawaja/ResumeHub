ocument.querySelectorAll('.search-box input').forEach(input => {
    input.addEventListener('input', function (e) {
        const searchTerm = e.target.value.toLowerCase();
        const portfolios = document.querySelectorAll('.portfolio-card');
        portfolios.forEach(card => {
            const title = card.querySelector('.portfolio-title').textContent.toLowerCase();
            card.style.display = title.includes(searchTerm) ? 'block' : 'none';
        });

        // Show empty state if no results
        const visible = document.querySelectorAll('.portfolio-card[style*="display: block"]');
        const emptyState = document.querySelector('.empty-state');
        if (visible.length === 0 && searchTerm !== '') {
            emptyState.style.display = 'block';
            emptyState.querySelector('h4').textContent = 'No matching projects found';
            emptyState.querySelector('p').textContent = 'Try a different search term or create a new project';
        } else {
            emptyState.style.display = 'none';
        }
    });
});