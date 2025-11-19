<template>
  <div class="chapter">
    <div class="chapterInfo">
      <h1>{{ seriesName }}</h1>
      <h2>{{ chapterName }}</h2>
      <h1>‧͙૮︵⭒‿᧔☪︎᧓‿⋆︵౨‧͙</h1>
      <div class="chapterButtons">
        <button class="prevChapter"><span>&#8678;</span></button>
        <button class="nextChapter"><span>&#8680;</span></button>
      </div>
      <div class="chapterContent">
          <p v-html="chapterContent"></p>
      </div>
    </div>

    <div class="chapterContent">
        <h1>{{ chapterName }}</h1>
        <p v-html="chapterContent"></p>
    </div>
    </div>
</template>

<script>
import { useRoute } from 'vue-router'
import { ref, onMounted } from 'vue'

export default {
  setup() {
    const route = useRoute()

    const seriesName = ref("")
    const chapterName = ref("")
    const chapterContent = ref("")
    const errorMessage = ref("")

    onMounted(async () => {
      const novelName = route.params.novelName;
      const chapterNumber = route.params.chapterNumber;

      if (!novelName || !chapterNumber) {
        errorMessage.value = "Brak nazwy noweli lub numeru rozdziału w adresie URL.";
        return;
      }

      try {
        const res = await fetch(`http://localhost:5193/api/chapter/novel/${novelName}/chapter/${chapterNumber}`);

        if (!res.ok) {
          throw new Error(`Błąd serwera: ${res.status}`);
        }

        const data = await res.json();
        seriesName.value = data.novel.title; 
        chapterName.value = data.title;
        chapterContent.value = data.content;
      } 
      catch (err) {
        console.error("Błąd pobierania:", err);
        errorMessage.value = "Nie udało się pobrać rozdziału. Sprawdź, czy backend działa poprawnie.";
      }
    })

    return {
      seriesName,
      chapterName,
      chapterContent,
      errorMessage
    }
  }
}
const chapterContent = ref("");

try {
  const response = await fetch("http://localhost:5193/api/chapter/1");
  const data = await response.json();
  chapterContent.value = data.content;
} catch (error) {
  console.error("Error fetching chapter content:", error);
}
</script>


<style lang="scss" scoped>
.chapter {
  padding: 2rem;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
}

.chapterInfo {
  background-color: var(--body-bg);
  color: var(--body-text);
  padding: 1.5rem;
  border-radius: 8px;
  h1 {
    text-align: center;
  }
  h2 {
    margin-top: 1rem;
    text-align: center;
  }
}

.chapterButtons {
  display: flex;
  justify-content: center;
  button {
    margin: 1rem;
    background-color: var(--button-bg);
    color: var(--button-text);
    border: none;
    padding: 0.75rem 1.5rem;
    border-radius: 6px;
    cursor: pointer;
    font-size: 1rem;
    box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
    transition: background-color 0.3s ease, color 0.3s ease;
    span {
      font-size: 1.5rem;
    }
    &:hover {
      background-color: var(--button-hover-bg);
      color: var(--button-hover-text);
    }
  }
}

.chapterContent {
  font-size: 1.2rem;
  margin-top: 2rem;
  line-height: 1.8;
  h2{
    text-align: left;
    font-size: 0.5rem;
  }
}
</style>
