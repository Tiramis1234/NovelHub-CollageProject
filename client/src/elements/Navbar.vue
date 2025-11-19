<template>
  <nav class="navbar">
    <div class="navbar-container">
      <RouterLink to="/" class="logo">
        <img src="../assets/logo.png" alt="Logo" class="logo-img" />
        <h1 class="logo-text">WebNovel Hub</h1>
      </RouterLink>

      <div class="search-container">
        <input
          type="text"
          v-model="searchQuery"
          placeholder="Search..."
          class="search-input"
        />
      </div>

      <button class="theme-toggle" @click="toggleTheme" aria-label="Toggle theme">
        <span v-if="theme === 'light'">🌙</span>
        <span v-else>☀️</span>
      </button>
    </div>
  </nav>
</template>

<script setup>
import { ref } from 'vue';
import { useTheme } from '../composables/useTheme';

const searchQuery = ref('');
const { theme, toggleTheme } = useTheme();
</script>

<style lang="scss">
.navbar {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  height: 60px;
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
  transition: background-color 0.25s, color 0.25s;
}

.navbar-container {
  width: 92%;
  max-width: 1200px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.logo {
  display: flex;
  align-items: center;
  text-decoration: none;
  color: inherit;

  .logo-img { height: 40px; margin-right: 8px; }
  .logo-text { font-size: 1.2rem; font-weight: 700; }
}

.search-container {
  flex: 1;
  max-width: 420px;
  margin: 0 1.5rem;
}

.search-input {
  width: 100%;
  padding: 0.45rem 0.7rem;
  border-radius: 8px;
  border: 1px solid;
  outline: none;
}

.theme-toggle {
  background: none;
  border: none;
  font-size: 1.4rem;
  cursor: pointer;
}
</style>
<dir></dir>