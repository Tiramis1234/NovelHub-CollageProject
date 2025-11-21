<template>
  <div class="series-container">
    <h1>Novel List</h1>
    

    <div v-if="loading" class="loading">Ładowanie biblioteki...</div>
    <div v-if="errorMessage" class="error">{{ errorMessage }}</div>


    <div v-if="!loading && novels.length > 0" class="series-grid">
      
      <div v-for="novel in novels" :key="novel.id" class="novel-card">
        

        <div class="novel-cover-wrapper">
           <img 
             :src="`/covers/${novel.id}.png`" 
             alt="Okładka" 
             loading="lazy"
             @error="handleImageError"
           />
        </div>

        <div class="card-content">
          <h2>{{ novel.title }}</h2>
          
          
          <p class="description">{{ truncateText(novel.description, 100) }}</p>
        </div>
        
        <div class="card-actions">

          <router-link 
            :to="{ name: 'novel', params: { novelName: getNovelSlug(novel.title) } }" 
            class="read-btn"
          >
            Read
          </router-link>
        </div>
      </div>

    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';

const novels = ref([]);
const loading = ref(false);
const errorMessage = ref("");


const handleImageError = (event) => {

  if (!event.target.src.includes('default.png')) {
    event.target.src = "/covers/default.png";
  }
};

const getNovelSlug = (title) => {
  if (!title) return "";
  return title.replace(/\s+/g, '-'); 
};

const truncateText = (text, length) => {
  if (!text) return "";
  if (text.length <= length) return text;
  return text.substring(0, length) + "...";
};

const fetchNovels = async () => {
  loading.value = true;
  try {
  
    const response = await fetch('http://localhost:5193/api/novel');
    
    if (!response.ok) {
      throw new Error("Nie udało się pobrać listy nowel.");
    }

    const data = await response.json();
    novels.value = data;
  } catch (err) {
    console.error(err);
    errorMessage.value = "Błąd serwera. Spróbuj odświeżyć stronę.";
  } finally {
    loading.value = false;
  }
};

onMounted(() => {
  fetchNovels();
});
</script>

<style lang="scss" scoped>
.series-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 2rem;
  margin-top: 60px; 
  color: var(--body-text);

  h1 {
    text-align: center;
    margin-bottom: 2rem;
    font-size: 2.5rem;
  }
}

.series-grid {
  display: grid;

  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  gap: 2rem;
}

.novel-card {
  background-color: var(--body-bg);
  border: 1px solid rgba(128,128,128,0.2);
  border-radius: 12px;
  overflow: hidden; 
  display: flex;
  flex-direction: column;
  transition: transform 0.3s, box-shadow 0.3s, border-color 0.3s;
  box-shadow: 0 4px 6px rgba(0,0,0,0.1);

  &:hover {
    transform: translateY(-5px);
    box-shadow: 0 12px 20px rgba(0,0,0,0.2);
    border-color: var(--accent-color, #3498db);


    .novel-cover-wrapper img {
      transform: scale(1.08);
    }
  }
}


.novel-cover-wrapper {
  width: 100%;
  height: 350px; 
  overflow: hidden;
  position: relative;
  background-color: #000; 

  img {
    width: 100%;
    height: 100%;
    object-fit: cover; 
    object-position: center top; 
    transition: transform 0.5s ease;
    display: block;
  }
}

.card-content {
  padding: 1.2rem;
  flex-grow: 1; 
  
  h2 {
    font-size: 1.3rem;
    margin-bottom: 0.5rem;
    color: var(--primary-text);
    line-height: 1.3;
  }

  .author {
    font-size: 0.9rem;
    color: #888;
    margin-bottom: 0.8rem;
    font-style: italic;
  }

  .description {
    font-size: 0.95rem;
    color: var(--body-text);
    opacity: 0.8;
    line-height: 1.5;
  }
}

.card-actions {
  padding: 1.2rem;
  padding-top: 0;
  display: flex;
  justify-content: flex-end;
}

.read-btn {
  background-color: var(--button-bg, #333);
  color: white;
  text-decoration: none;
  padding: 0.6rem 1.5rem;
  border-radius: 20px;
  font-weight: 600;
  font-size: 0.9rem;
  transition: background 0.3s;
  border: 1px solid transparent;

  &:hover {
    background-color: var(--accent-color, #3498db);
  }
}

.loading, .error {
  text-align: center;
  font-size: 1.2rem;
  margin-top: 3rem;
}
.error {
  color: #e74c3c;
}
@media (max-width: 600px) {
  .novel-cover-wrapper {
    height: 300px; 
  }
}
</style>