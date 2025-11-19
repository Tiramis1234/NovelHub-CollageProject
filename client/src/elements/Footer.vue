
<template>
    <div class="footerContainer">
        <div class="logos">
            <div v-if="theme === 'dark'">
                <img src="../assets/gitDark.png" alt="" srcset="">
            </div>
            <div v-else>
                <img src="../assets/gitLight.png" alt="">
            </div>
        </div>
    </div>

    <div class="copyright"></div>
</template>

<script setup>
import { useTheme } from '../composables/useTheme';
const { theme } = useTheme();
</script>

<style lang="scss">
.footerContainer {
    display: flex;
    justify-content: space-between;
        padding: 1rem 2rem;
        background-color: var(--footer-bg);
        color: var(--footer-text);
        border-top: 1px solid var(--navbar-border);
        transition: background-color 0.25s ease, border-color 0.25s ease, color 0.25s ease;
}

.copyright {
    padding: 0.75rem 2rem;
    text-align: center;
    background-color: var(--footer-bg);
    color: var(--footer-text);
    border-top: 1px solid var(--navbar-border);
    transition: background-color 0.25s ease, color 0.25s ease, border-color 0.25s ease;
}
.logos {
    display: flex;
    gap: 1.5rem;
}
img {
    height: 30px;
    width: 30px;
}
</style>