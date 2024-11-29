let currentIndex = 0;

function moveSlide(direction) {
    const slides = document.querySelectorAll('.carousel-item');
    const totalSlides = slides.length;

    // Update current index based on direction
    currentIndex = (currentIndex + direction + totalSlides) % totalSlides;

    // Update the transform property of the carousel to shift the slides
    const carousel = document.querySelector('.carousel');
    carousel.style.transform = `translateX(-${currentIndex * 100}%)`;
}

// Optional: Automatically transition every 3 seconds
setInterval(() => moveSlide(1), 3000);