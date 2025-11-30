<template>
  <div class="login-page">
    <div class="login-card">
      
      <div class="login-title">
        <h1>Sign In</h1>
      </div>

      <form @submit.prevent="handleLogin" class="login-form">
        <div class="input-group">
          <label for="email">Email</label>
          <input 
            type="text" 
            id="email"
            class="login-input" 
            placeholder="Email"
            v-model="email"
          >
        </div>

        <div class="input-group">
          <label for="password">Hasło</label>
          <input 
            type="password" 
            id="password"
            class="login-input" 
            placeholder="Password"
            v-model="password"
          >
        </div>

        <button type="submit" class="btn-login">Login</button>
        
        <a href="#" class="forgot-pass">Forgot Password?</a>
      </form>

      <hr class="divider">

      <div class="register-section">
        <p>Sign up</p>
        <button class="btn-register">Register</button>
      </div>

    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';

const email = ref('');
const password = ref('');

const handleLogin = () => {

  console.log('Logowanie:', email.value, password.value);
};
</script>

<style lang="scss" scoped>
.login-page {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 80vh; 
  margin-top: 60px;
  padding: 1rem;
}


.login-card {
  width: 100%;
  max-width: 400px;
  background-color: var(--login-bg); 
  border: 1px solid rgba(128, 128, 128, 0.2);
  border-radius: 12px;
  padding: 2.5rem;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
  color: var(--body-text);
  text-align: center;
}

.login-title h1 {
  margin-bottom: 2rem;
  font-size: 2rem;
  font-weight: 700;
}
.login-form {
  display: flex;
  flex-direction: column;
  gap: 1.2rem;
}

.input-group {
  text-align: left;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  
  label {
    font-size: 0.9rem;
    font-weight: 600;
    margin-left: 5px;
  }
}


.login-input {
  width: 90%;
  padding: 0.8rem 1.2rem;
  border-radius: 25px;
  border: 1px solid rgba(128, 128, 128, 0.4);
  background-color: var(--search-bg, #fff);
  color: var(--input-text, #000);
  outline: none;
  font-size: 1rem;
  transition: border-color 0.3s, box-shadow 0.3s;

  &:focus {
    border-color: var(--accent-color, #3498db);
    box-shadow: 0 0 0 3px rgba(52, 152, 219, 0.2);
  }
}


.btn-login {
  margin-top: 1rem;
  padding: 0.8rem;
  border-radius: 25px;
  border: none;
  background-color: var(--accent-color, #3498db);
  color: white;
  font-size: 1.1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.3s, transform 0.2s;

  &:hover {
    background-color: var(--button-hover-bg, #2980b9);
    transform: translateY(-2px);
  }
}

.forgot-pass {
  font-size: 0.9rem;
  color: #888;
  text-decoration: none;
  margin-top: 0.5rem;
  
  &:hover {
    text-decoration: underline;
    color: var(--accent-color, #3498db);
  }
}

.divider {
  margin: 2rem 0;
  border: 0;
  border-top: 1px solid rgba(128, 128, 128, 0.2);
}


.register-section {
  p {
    margin-bottom: 0.5rem;
    color: #888;
  }
}

.btn-register {
  padding: 0.6rem 1.5rem;
  border-radius: 25px;
  border: 1px solid var(--accent-color, #3498db);
  background: transparent;
  color: var(--accent-color, #3498db);
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s;

  &:hover {
    background-color: var(--accent-color, #3498db);
    color: white;
  }
}
</style>