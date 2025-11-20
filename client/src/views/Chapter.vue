<template>
  <div class="chapter">

    <div v-if="errorMessage" class="error">{{ errorMessage }}</div>
    <div v-if="loading" class="loading">Ładowanie rozdziału...</div>

 
    <div v-if="!loading && !errorMessage" class="chapterInfo">
      
    
      <h1 class="series-title" @click="goToSeries">
        {{ seriesName }}
      </h1>
      
  
      <h2>{{ chapterTitle }}</h2>
      
      <div class="separator">‧͙૮︵⭒‿᧔☪︎᧓‿⋆︵౨‧͙</div>

  
      <div class="chapterButtons">
        <button 
          @click="changeChapter(prevChapterNum)" 
          :disabled="!prevChapterNum" 
          :class="{ disabled: !prevChapterNum }">
          <span>&#8678;</span> 
        </button>

        <button 
          @click="changeChapter(nextChapterNum)" 
          :disabled="!nextChapterNum"
          :class="{ disabled: !nextChapterNum }">
          <span>&#8680;</span>
        </button>
      </div>

   
      <div class="chapterContent">
          <p v-html="chapterContent"></p>
      </div>

    
      <div class="chapterButtons">
        <button 
          @click="changeChapter(prevChapterNum)" 
          :disabled="!prevChapterNum"
          :class="{ disabled: !prevChapterNum }">
          <span>&#8678;</span> 
        </button>

        <button 
          @click="changeChapter(nextChapterNum)" 
          :disabled="!nextChapterNum"
          :class="{ disabled: !nextChapterNum }">
          <span>&#8680;</span>
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();


const seriesName = ref("");
const chapterTitle = ref("");
const chapterContent = ref("");
const nextChapterNum = ref(null);
const prevChapterNum = ref(null);

const loading = ref(false);
const errorMessage = ref("");

const fetchChapterData = async (novelName, chapterNumber) => {
  loading.value = true;
  errorMessage.value = "";
  
  try {

    const response = await fetch(`http://localhost:5193/api/chapter/novel/${novelName}/chapter/${chapterNumber}`);

    if (!response.ok) {
      throw new Error("Nie znaleziono rozdziału lub błąd serwera.");
    }

    const data = await response.json();

 
    seriesName.value = data.novelTitle; 
    chapterTitle.value = `Rozdział ${data.chapterNumber}: ${data.title}`;
    chapterContent.value = data.content;
    

    nextChapterNum.value = data.nextChapterNumber;
    prevChapterNum.value = data.prevChapterNumber;

  } catch (err) {
    console.error(err);
    errorMessage.value = "Błąd podczas ładowania rozdziału.";
  } finally {
    loading.value = false;
  }
};

// Funkcja nawigacji do innego rozdziału
const changeChapter = (newChapterNum) => {
  if (newChapterNum) {
    router.push({
      name: 'chapter',
      params: {
        novelName: route.params.novelName,
        chapterNumber: newChapterNum
      }
    });

    window.scrollTo(0, 0);
  }
};


const goToSeries = () => {
  router.push({
    name: 'novel',
    params: { novelName: route.params.novelName }
  });
};


onMounted(() => {
  fetchChapterData(route.params.novelName, route.params.chapterNumber);
});
watch(
  () => [route.params.novelName, route.params.chapterNumber],
  ([newNovel, newChapter]) => {
    if (newNovel && newChapter) {
      fetchChapterData(newNovel, newChapter);
    }
  }
);
</script>

<style lang="scss" scoped>
.chapter {
  padding: 2rem;
  margin: 0 auto;
  max-width: 800px;
}

.error {
  color: red;
  text-align: center;
  font-size: 1.5rem;
}

.loading {
  text-align: center;
  font-size: 1.2rem;
  color: var(--body-text);
}

.chapterInfo {
  background-color: var(--body-bg);
  color: var(--body-text);
  padding: 1.5rem;
  border-radius: 8px;
}

.series-title {
  text-align: center;
  cursor: pointer;
  color: var(--link-color, #3498db);
  &:hover {
    text-decoration: underline;
  }
}

h2 {
  margin-top: 0.5rem;
  text-align: center;
  font-size: 1.5rem;
}

.separator {
  text-align: center;
  margin: 1rem 0;
  font-size: 1.5rem;
  color: #888;
}

.chapterButtons {
  display: flex;
  justify-content: space-between;
  margin: 2rem 0;

  button {
    background-color: var(--button-bg, #444);
    color: var(--button-text, #fff);
    border: none;
    padding: 0.75rem 1.5rem;
    border-radius: 6px;
    cursor: pointer;
    font-size: 1rem;
    display: flex;
    align-items: center;
    gap: 10px;
    transition: opacity 0.3s;

    &:hover:not(.disabled) {
      background-color: var(--button-hover-bg, #666);
    }

    &.disabled {
      opacity: 0.5;
      cursor: not-allowed;
      background-color: #333;
    }
  }
}

.chapterContent {
  font-size: 1.2rem;
  line-height: 1.8;
  text-align: justify;
  white-space: pre-wrap;
}
</style>