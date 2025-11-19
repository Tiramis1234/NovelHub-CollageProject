<script setup>
import Navbar from './elements/Navbar.vue';
import Foter from './elements/Footer.vue';
import { RouterView } from 'vue-router';
import router from './router';
import { useTheme } from './composables/useTheme';
const { theme } = useTheme();
</script>

<template>
  <div>
    <Navbar />
    <div class="nav-spacer" aria-hidden="true"></div>
    <main class="main-content">
        <router-view />
    </main>
    <Foter />
  </div>
</template>

<style lang="scss">
@use './styles/main.scss';

body {
  margin: 0;
  padding: 0;
  min-height: 100vh;
  transition: background-color 0.3s ease, color 0.3s ease;
}

.app-container {
  min-height: 100vh;
}

body {
  background-color: var(--body-bg);
  color: var(--body-text);
}

.nav-spacer {
  height: 60px; 
  background: var(--navbar-bg);
  border-bottom: 1px solid var(--navbar-border);
  transition: background-color 0.25s ease, border-color 0.25s ease;
}
</style>
