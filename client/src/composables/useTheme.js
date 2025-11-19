import { ref } from 'vue'

const theme = ref(localStorage.getItem('theme') || 'light')

const applyTheme = (newTheme) => {
  if (newTheme === 'dark') {
    document.body.classList.add('dark')
  } else {
    document.body.classList.remove('dark')
  }
}

applyTheme(theme.value)

export function useTheme() {
  const toggleTheme = () => {
    theme.value = theme.value === 'light' ? 'dark' : 'light'
    localStorage.setItem('theme', theme.value)
    applyTheme(theme.value)
  }

  return {
    theme,
    toggleTheme
  }
}