<template>
  <div class="novel-page">
    
    <div v-if="loading" class="status-container">
      <div class="spinner"></div>
      <p>Ładowanie noweli...</p>
    </div>


    <div v-else-if="errorMessage" class="status-container error">
      <h2>Ojej! Coś poszło nie tak.</h2>
      <p>{{ errorMessage }}</p>
      <router-link to="/series" class="back-btn">Wróć do listy</router-link>
    </div>


    <div v-else-if="novel" class="content-wrapper">
      

      <div class="novel-header">
        <div class="cover-section">
          <img 
            :src="`/covers/${novel.id}.png`" 
            alt="Okładka noweli" 
            @error="handleImageError"
            class="novel-cover"
          />
        </div>
        <div class="info-section">
          <h1 class="novel-title">{{ novel.title }}</h1>

          <div class="actions">
            <button 
              @click="startReading" 
              class="action-btn primary"
              :disabled="!novel.chapters || novel.chapters.length === 0">
              Read Now
            </button>
            <!--Boockmark-->
          </div>

          <div class="description-box">
            <h3>Description</h3>
            <p>{{ novel.description || 'Brak opisu dla tej noweli.' }}</p>
          </div>
        </div>
      </div>

      <hr class="divider" />

      <div class="chapters-section">
        <h3>Chapters ({{ novel.chapters.length }})</h3>
        
        <div v-if="novel.chapters.length > 0" class="chapters-grid">
          <button 
            v-for="chapter in novel.chapters" 
            :key="chapter.chapterNumber"
            class="chapter-card"
            @click="goToChapter(chapter.chapterNumber)"
          >
            <span class="chap-num">Rozdział {{ chapter.chapterNumber }}</span>
            <span class="chap-title">{{ chapter.title }}</span>
          </button>
        </div>

        <div v-else class="no-chapters">
          <p>Autora dopadło lenistwo – brak rozdziałów.</p>
        </div>
      </div>

    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';


const API_URL = "http://localhost:5193";

const route = useRoute();
const router = useRouter();

const novel = ref(null);
const loading = ref(true);
const errorMessage = ref("");

const fetchNovelData = async () => {
  loading.value = true;
  errorMessage.value = "";
  const slug = route.params.novelName;

  try {

    const response = await fetch(`${API_URL}/api/novel/${slug}`);

    if (!response.ok) {
      if (response.status === 404) throw new Error("Nie znaleziono takiej noweli.");
      throw new Error("Błąd połączenia z serwerem.");
    }

    const data = await response.json();
    novel.value = data;
  } catch (err) {
    console.error(err);
    errorMessage.value = err.message;
  } finally {
    loading.value = false;
  }
};


const getCoverUrl = (id) => {
  return `${API_URL}/covers/${id}.jpg`;
};

const handleImageError = (event) => {

  if (!event.target.src.includes('default.jpg')) {
    event.target.src = `${API_URL}/covers/default.jpg`;
  }
};


const goToChapter = (num) => {
  router.push({
    name: 'chapter',
    params: {
      novelName: route.params.novelName,
      chapterNumber: num
    }
  });
};

const startReading = () => {
  if (novel.value && novel.value.chapters.length > 0) {
    const firstChapter = novel.value.chapters.reduce((prev, curr) => 
      prev.chapterNumber < curr.chapterNumber ? prev : curr
    );
    goToChapter(firstChapter.chapterNumber);
  }
};

onMounted(() => {
  fetchNovelData();
});

watch(() => route.params.novelName, () => {
  fetchNovelData();
});
</script>

<style lang="scss" scoped>
.novel-page {
  max-width: 1100px;
  margin: 0 auto;
  padding: 2rem 1rem;

  margin-top: 80px; 
  min-height: 80vh;
  color: var(--body-text);
}

.novel-header {
  display: flex;
  gap: 3rem;
  margin-bottom: 3rem;

  @media (max-width: 768px) {
    flex-direction: column;
    align-items: center;
    gap: 1.5rem;
  }
}

.cover-section {
  flex-shrink: 0;
  
  .novel-cover {
    width: 280px;
    height: 400px;
    object-fit: cover;
    border-radius: 12px;
    box-shadow: 0 8px 20px rgba(0,0,0,0.4);
    transition: transform 0.3s;

    &:hover {
      transform: scale(1.02);
    }
  }

  @media (max-width: 768px) {
    .novel-cover {
      width: 200px;
      height: 300px;
    }
  }
}

.info-section {
  flex-grow: 1;
  display: flex;
  flex-direction: column;

  .novel-title {
    font-size: 2.5rem;
    font-weight: 800;
    margin-bottom: 0.5rem;
    line-height: 1.2;
    color: var(--primary-text);
  }

  .novel-author {
    font-size: 1.1rem;
    color: #888;
    margin-bottom: 1.5rem;
    font-style: italic;
  }

  .actions {
    margin: 1.5rem 0;
    
    .action-btn {
      padding: 0.8rem 2rem;
      font-size: 1.1rem;
      border-radius: 30px;
      border: none;
      cursor: pointer;
      font-weight: 600;
      transition: transform 0.2s, box-shadow 0.2s;

      &.primary {
        background-color: var(--accent-color, #3498db);
        color: white;
        box-shadow: 0 4px 15px rgba(52, 152, 219, 0.3);

        &:hover {
          background-color: var(--button-hover-bg, #2980b9);
          transform: translateY(-2px);
        }

        &:disabled {
          background-color: #555;
          cursor: not-allowed;
          box-shadow: none;
        }
      }
    }
  }

  .description-box {
    margin-top: auto;
    
    h3 {
      font-size: 1.3rem;
      margin-bottom: 0.5rem;
      border-bottom: 2px solid var(--accent-color, #3498db);
      display: inline-block;
    }

    p {
      line-height: 1.6;
      font-size: 1rem;
      opacity: 0.9;
      white-space: pre-wrap;
    }
  }
}

.divider {
  border: 0;
  height: 1px;
  background: linear-gradient(to right, transparent, rgba(128,128,128,0.3), transparent);
  margin: 2rem 0;
}

.chapters-section {
  h3 {
    margin-bottom: 1.5rem;
    font-size: 1.5rem;
  }
}

.chapters-grid {
  display: grid;

  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 1rem;
}

.chapter-card {
  background-color: var(--card-bg, rgba(255,255,255,0.05));
  border: 1px solid rgba(128,128,128,0.2);
  padding: 1rem;
  border-radius: 8px;
  cursor: pointer;
  text-align: left;
  display: flex;
  align-items: center;
  gap: 10px;
  transition: all 0.2s ease;
  color: inherit;

  &:hover {
    background-color: var(--card-hover-bg, rgba(255,255,255,0.1));
    border-color: var(--accent-color, #3498db);
    transform: translateX(5px);
  }

  .chap-num {
    font-weight: bold;
    color: var(--accent-color, #3498db);
    white-space: nowrap;
  }

  .chap-title {
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
  }
}

.status-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 50vh;
  text-align: center;

  &.error {
    color: #e74c3c;
  }
}

.spinner {
  border: 4px solid rgba(0, 0, 0, 0.1);
  width: 50px;
  height: 50px;
  border-radius: 50%;
  border-left-color: var(--accent-color, #3498db);
  animation: spin 1s linear infinite;
  margin-bottom: 1rem;
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}

.back-btn {
  margin-top: 1rem;
  color: var(--accent-color);
  text-decoration: underline;
  cursor: pointer;
}
</style>