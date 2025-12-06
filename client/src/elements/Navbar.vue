<template>
  <nav class="navbar" :class="{ 'navbar-hidden': isNavbarHidden }">
    <div class="navbar-container">
      
      <RouterLink to="/" class="logo">
        <img src="../assets/logo.png" alt="Logo" class="logo-img" />
        <h1 class="logo-text">WebNovel Hub</h1>
      </RouterLink>

      <div class="mobile-controls">
        <button class="theme-toggle mobile-only" @click="toggleTheme" aria-label="Toggle theme">
          <span v-if="theme === 'light'">🌙</span>
          <span v-else>☀️</span>
        </button>

        <button class="hamburger" @click="toggleMenu" aria-label="Menu">
          <span :class="{ 'open': isMenuOpen }">☰</span>
        </button>
      </div>

      <div class="nav-menu" :class="{ 'active': isMenuOpen }">
        <div class="nav-links">
          <RouterLink to="/" class="nav-link" @click="closeMenu">Home</RouterLink>
          <RouterLink to="/series" class="nav-link" @click="closeMenu">Series</RouterLink>
          
          <RouterLink 
            v-if="isLoggedIn" 
            to="/profile" 
            class="nav-link" 
            @click="closeMenu"
          >
            Profile
          </RouterLink>
          
          <RouterLink 
            v-else 
            to="/login" 
            class="nav-link" 
            @click="closeMenu"
          >
            Login
          </RouterLink>
        </div>

        <button class="theme-toggle desktop-only" @click="toggleTheme" aria-label="Toggle theme">
          <span v-if="theme === 'light'">🌙</span>
          <span v-else>☀️</span>
        </button>
      </div>

    </div>
  </nav>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue';
import { useTheme } from '../composables/useTheme';

const isMenuOpen = ref(false);
const { theme, toggleTheme } = useTheme();
const isLoggedIn = ref(false); 


const isNavbarHidden = ref(false);
const lastScrollPosition = ref(0);

const handleScroll = () => {

  const currentScrollPosition = window.scrollY || document.documentElement.scrollTop;


  if (isMenuOpen.value) {
    return;
  }


  if (currentScrollPosition < 0) {
    return;
  }

  // Jeśli jesteśmy na samej górze, zawsze pokazuj
  if (currentScrollPosition === 0) {
    isNavbarHidden.value = false;
    lastScrollPosition.value = 0;
    return;
  }

  if (Math.abs(currentScrollPosition - lastScrollPosition.value) > 60) {
    isNavbarHidden.value = currentScrollPosition > lastScrollPosition.value;
    lastScrollPosition.value = currentScrollPosition;
  }
};

const toggleMenu = () => {
  isMenuOpen.value = !isMenuOpen.value;
};

const closeMenu = () => {
  isMenuOpen.value = false;
};


onMounted(() => {
  window.addEventListener('scroll', handleScroll);
});


onUnmounted(() => {
  window.removeEventListener('scroll', handleScroll);
});
</script>

<style lang="scss" scoped>

.navbar {
  position: fixed; 
  top: 0;
  left: 0;
  right: 0;
  width: 100%;
  height: 70px;
  background-color: var(--body-bg);
  color: var(--body-text);
  border-bottom: 1px solid rgba(128, 128, 128, 0.2);
  z-index: 1000;
  

  transition: transform 0.3s ease-in-out, background-color 0.3s, color 0.3s;
}


.navbar-hidden {
  transform: translateY(-100%); 
}

.navbar-container {
  width: 92%;
  max-width: 1200px;
  height: 100%;
  margin: 0 auto;
  display: flex;
  align-items: center;
  justify-content: space-between;
  flex-wrap: wrap; 
}

.logo {
  display: flex;
  align-items: center;
  text-decoration: none;
  color: inherit;
  z-index: 1001;

  .logo-img { height: 40px; margin-right: 12px; }
  .logo-text { font-size: 1.4rem; font-weight: 800; white-space: nowrap; letter-spacing: 0.5px; }
}

.hamburger, .mobile-only {
  display: none;
}

.nav-menu {
  display: flex;
  align-items: center;
}

.nav-links {
  display: flex;
  align-items: center;
  gap: 30px; 
  margin-right: 30px;
}

.nav-link {
  text-decoration: none;
  color: inherit;
  font-weight: 600;
  font-size: 1.1rem;
  position: relative; 
  transition: color 0.3s ease, transform 0.2s ease;
  padding: 5px 0;

  &::after {
    content: '';
    position: absolute;
    width: 0;
    height: 2px;
    bottom: 0;
    left: 50%;
    background-color: var(--accent-color, #3498db);
    transition: width 0.3s ease, left 0.3s ease;
  }

  &:hover {
    color: var(--accent-color, #3498db);
    transform: translateY(-2px);
  }

  &:hover::after {
    width: 100%;
    left: 0;
  }
  
  &.router-link-active {
    color: var(--accent-color, #3498db);
    &::after {
      width: 100%;
      left: 0;
    }
  }
}

.theme-toggle {
  background: none;
  border: none;
  font-size: 1.5rem;
  cursor: pointer;
  padding: 5px;
  color: inherit;
  transition: transform 0.3s;
  
  &:hover {
    transform: rotate(15deg) scale(1.1);
  }
}


@media (max-width: 768px) {
  .navbar {
    height: 60px; 
  }
  
  .navbar-container {
    padding: 0; 
  }

  .mobile-controls {
    display: flex;
    align-items: center;
    gap: 15px;
  }
  
  .hamburger, .mobile-only {
    display: block;
  }
  
  .desktop-only {
    display: none;
  }

  .hamburger {
    background: none;
    border: none;
    font-size: 1.8rem;
    cursor: pointer;
    color: inherit;
    padding: 0 5px;
  }

  .nav-menu {
    position: absolute;
    top: 60px;
    left: 0;
    width: 100%;
    background-color: var(--body-bg);
    border-bottom: 1px solid rgba(128,128,128, 0.2);
    flex-direction: column;
    align-items: flex-start;
    padding: 0;
    gap: 15px; 
    
    max-height: 0;
    overflow: hidden;
    transition: max-height 0.3s ease-in-out, padding 0.3s ease;
    box-shadow: 0 10px 20px rgba(0,0,0,0.1);
  }

  .nav-menu.active {
    max-height: 400px;
    padding: 20px;
  }

  .nav-links {
    flex-direction: column;
    width: 100%;
    gap: 10px; 
    margin-bottom: 10px;
    margin-right: 0;
  }

  .nav-link {
    width: 100%;
    display: block;
    padding: 10px 0;
    border-bottom: 1px solid rgba(128,128,128, 0.1);
    
    &:hover {
      transform: none; 
    }
    &::after {
      display: none; 
    }
  }
}
</style>