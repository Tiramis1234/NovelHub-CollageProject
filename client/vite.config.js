import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

export default defineConfig({
  plugins: [vue()],
  server: {
    target: 'https://localhost:5193',
    changeOrigin: true,
    secure: false
  }

})
