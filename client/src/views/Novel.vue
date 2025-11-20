<template>
  <div class="novel-container">
    <div v-if="loading" class="loading">Ładowanie danych...</div>
    <div v-if="errorMessage" class="error">{{ errorMessage }}</div>

    <div v-if="!loading && novelData" class="novel-details">

      <div class="novel-header">
        <h1>{{ novelData.title }}</h1>
        <p class="description">{{ novelData.description || 'Brak opisu.' }}</p>
      </div>

      <hr class="divider" />

 
      <div class="chapter-list">
        <h3>Dostępne rozdziały:</h3>
        <ul>
          <li v-for="chapter in novelData.chapters" :key="chapter.chapterNumber">
            <button class="chapter-link" @click="goToChapter(chapter.chapterNumber)">
              <span class="ch-num">Rozdział {{ chapter.chapterNumber }}</span>
              <span class="ch-title">{{ chapter.title }}</span>
            </button>
          </li>
        </ul>
        <div v-if="novelData.chapters.length === 0">Brak rozdziałów.</div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();

const novelData = ref(null);
const loading = ref(false);
const errorMessage = ref("");

const fetchNovel = async () => {
  const novelName = route.params.novelName;
  loading.value = true;

  try {

    const response = await fetch(`http://localhost:5193/api/novel/${novelName}`);
    
    if (!response.ok) throw new Error("Nie udało się pobrać noweli.");

    const data = await response.json();
    novelData.value = data; 
  } catch (err) {
    console.error(err);
    errorMessage.value = "Błąd podczas pobierania danych noweli.";
  } finally {
    loading.value = false;
  }
};

const goToChapter = (chapterNum) => {
  router.push({
    name: 'chapter',
    params: {
      novelName: route.params.novelName,
      chapterNumber: chapterNum
    }
  });
};

onMounted(() => {
  fetchNovel();
});
</script>

<style lang="scss" scoped>
.novel-container {
  max-width: 900px;
  margin: 0 auto;
  padding: 2rem;
  color: var(--body-text);
}

.novel-header {
  text-align: center;
  margin-bottom: 2rem;
  
  h1 {
    font-size: 2.5rem;
    margin-bottom: 1rem;
  }
}

.divider {
  border: 0;
  height: 1px;
  background: #444;
  margin: 2rem 0;
}

.chapter-list {
  ul {
    list-style: none;
    padding: 0;
  }

  li {
    margin-bottom: 0.5rem;
  }

  .chapter-link {
    width: 100%;
    display: flex;
    justify-content: flex-start;
    gap: 1rem;
    padding: 1rem;
    background-color: var(--body-bg); /* Dostosuj do swojego theme */
    border: 1px solid #444;
    color: inherit;
    cursor: pointer;
    text-align: left;
    border-radius: 4px;
    transition: background 0.2s;

    &:hover {
      background-color: var(--button-hover-bg, #333);
    }

    .ch-num {
      font-weight: bold;
      color: var(--accent-color, #3498db);
      min-width: 100px;
    }
  }
}

.error { color: red; text-align: center; }
.loading { text-align: center; }
</style>