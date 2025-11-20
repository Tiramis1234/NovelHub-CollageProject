<template>
  <div class="home-page-container">
    <section class="daily-reccomendations">
      <h2>Daily Recommendations</h2>
      <!-- Daily recommendations content goes here -->
    </section>

    <section class="daily-listens">
      <h2>Hot Picks</h2>
      <ul>
        <li @click="h1='sigma'">1</li>
        <li>2</li>
        <li>3</li>
      </ul>
    </section>
  </div>
</template>

<script>
import { ref } from 'vue';

</script>

<style lang="scss" scoped>
.home-page-container {
  padding: 80px 20px 20px 20px;
  max-width: 1200px;
  margin: 0 auto;
  display: flex;
  flex-direction: row;

}
.daily-reccomendations {
  margin: 1rem;
  flex: 2;

}
.daily-listens {
  margin: 1rem;
  flex: 1;
  h2{
    text-align: center;
  }
  ul{
    list-style: none;
    padding: 0;
  }
  li{
    background-color: #f0f0f0;
    margin: 0.5rem 0;
    padding: 0.75rem;
    border-radius: 8px;
    box-shadow: inset 0 0 5px rgba(0, 0, 0, 0.1);
  };
  li:hover{
      background-color: #fcfcfc;
      text-decoration: underline;
    }
}

</style>