<template>
  <div class="home-container">
    
    <section class="hero">
      <div class="hero-content">
        <h1 class="animate-fade-up">Odkryj Nieskończone Światy</h1>
        <p class="subtitle animate-fade-up delay-1">
          Tysiące historii w Twoim zasięgu. Zanurz się w najlepszych nowelach webowych, 
          od fantasy po sci-fi, całkowicie za darmo.
        </p>
        <div class="cta-group animate-fade-up delay-2">
          <router-link to="/series" class="btn btn-primary">Rozpocznij Czytanie</router-link>
        </div>
      </div>
      
      <div class="hero-background"></div>
    </section>

    <section class="section trending">
      <div class="section-header">
        <h2>🔥 Na czasie</h2>
        <router-link to="/series" class="view-all">Zobacz wszystkie &rarr;</router-link>
      </div>

      <div v-if="loading" class="loading-state">Ładowanie noweli...</div>
      
      <div v-else class="trending-grid">
        <div 
          v-for="novel in featuredNovels" 
          :key="novel.id" 
          class="novel-card-modern"
          @click="goToNovel(novel.title)"
        >
          <div class="card-image">
            <img 
              :src="`/covers/${novel.id}.png`" 
              @error="handleImageError" 
              alt="Cover" 
            />
            <div class="overlay">
              <span class="read-icon">📖</span>
            </div>
          </div>
          <div class="card-info">
            <h3>{{ novel.title }}</h3>
            <p class="summary">{{ truncateText(novel.description, 60) }}</p>
          </div>
        </div>
      </div>
    </section>

    <section class="section features">
      <div class="feature-box">
        <div class="icon">📚</div>
        <h3>Ogromna Biblioteka</h3>
        <p>Dostęp do setek tytułów w jednym miejscu.</p>
      </div>
      <div class="feature-box">
        <div class="icon">⚡</div>
        <h3>Szybkie Aktualizacje</h3>
        <p>Nowe rozdziały dodawane każdego dnia.</p>
      </div>
      <div class="feature-box">
        <div class="icon">🌙</div>
        <h3>Tryb Nocny</h3>
        <p>Czytaj wygodnie o każdej porze dnia i nocy.</p>
      </div>
    </section>


  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue';
import { useRouter } from 'vue-router';


const router = useRouter();
const novels = ref([]);
const loading = ref(true);

const featuredNovels = computed(() => {
  return novels.value.slice(0, 4);
});

const fetchNovels = async () => {
  try {
    const response = await fetch('http://localhost:5193/api/novel');
    if (!response.ok) throw new Error("Błąd pobierania");
    const data = await response.json();
    novels.value = data;
  } catch (err) {
    console.error(err);
  } finally {
    loading.value = false;
  }
};

const handleImageError = (event) => {
  if (!event.target.src.includes('default.png')) {
    event.target.src = "/covers/default.png";
  }
};

const truncateText = (text, length) => {
  if (!text) return "";
  return text.length > length ? text.substring(0, length) + "..." : text;
};

const getNovelSlug = (title) => title ? title.replace(/\s+/g, '-') : "";

const goToNovel = (title) => {
  router.push({ name: 'novel', params: { novelName: getNovelSlug(title) } });
};

onMounted(() => {
  fetchNovels();
});
</script>

<style lang="scss" scoped>
.home-container {
  width: 100%;
  overflow-x: hidden;
  color: var(--body-text);
  margin-top: 60px;
}

/* --- HERO SECTION --- */
.hero {
  position: relative;
  min-height: 80vh;
  display: flex;
  align-items: center;
  justify-content: center;
  text-align: center;
  padding: 2rem;
  overflow: hidden;
  background: radial-gradient(circle at top right, rgba(52, 152, 219, 0.1), transparent 40%),
              radial-gradient(circle at bottom left, rgba(52, 152, 219, 0.05), transparent 40%);
}

.hero-content {
  z-index: 2;
  max-width: 800px;
  
  h1 {
    font-size: 3.5rem;
    font-weight: 800;
    margin-bottom: 1.5rem;
    line-height: 1.1;
    background: linear-gradient(135deg, var(--body-text) 0%, var(--accent-color) 100%);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
  }

  .subtitle {
    font-size: 1.25rem;
    margin-bottom: 2.5rem;
    opacity: 0.8;
    line-height: 1.6;
  }
}

.cta-group {
  display: flex;
  gap: 1.5rem;
  justify-content: center;
  flex-wrap: wrap;

  .btn {
    padding: 0.8rem 2.5rem;
    border-radius: 50px;
    font-weight: 600;
    font-size: 1rem;
    text-decoration: none;
    transition: transform 0.3s, box-shadow 0.3s;
    cursor: pointer;

    &:hover {
      transform: translateY(-3px);
      box-shadow: 0 10px 20px rgba(0,0,0,0.1);
    }
  }

  .btn-primary {
    background-color: var(--accent-color);
    color: white;
    border: none;
    box-shadow: 0 4px 15px rgba(52, 152, 219, 0.3);
  }
}

/* --- SECTIONS COMMON --- */
.section {
  max-width: 1200px;
  margin: 0 auto;
  padding: 4rem 2rem;

  .section-header {
    display: flex;
    justify-content: space-between;
    align-items: flex-end;
    margin-bottom: 2rem;

    h2 {
      font-size: 2rem;
      font-weight: 700;
      position: relative;
      display: inline-block;
      
      &::after {
        content: '';
        position: absolute;
        bottom: -5px;
        left: 0;
        width: 40px;
        height: 4px;
        background-color: var(--accent-color);
        border-radius: 2px;
      }
    }

    .view-all {
      color: var(--accent-color);
      text-decoration: none;
      font-weight: 600;
      transition: opacity 0.2s;
      &:hover { opacity: 0.8; }
    }
  }
}

/* --- TRENDING GRID --- */
.trending-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
  gap: 2rem;
}

.novel-card-modern {
  background-color: var(--card-bg, rgba(255,255,255,0.05));
  border-radius: 16px;
  overflow: hidden;
  cursor: pointer;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  border: 1px solid rgba(128,128,128,0.1);
  box-shadow: 0 4px 10px rgba(0,0,0,0.05);

  &:hover {
    transform: translateY(-8px);
    box-shadow: 0 15px 30px rgba(0,0,0,0.15);
    border-color: var(--accent-color);

    .card-image img { transform: scale(1.05); }
    .overlay { opacity: 1; }
  }

  .card-image {
    height: 320px;
    width: 100%;
    position: relative;
    overflow: hidden;

    img {
      width: 100%; height: 100%; object-fit: cover; transition: transform 0.5s ease;
    }

    .overlay {
      position: absolute; inset: 0;
      background: rgba(0,0,0,0.4);
      display: flex; align-items: center; justify-content: center;
      opacity: 0; transition: opacity 0.3s;
      
      .read-icon { font-size: 3rem; color: white; transform: scale(0.8); transition: transform 0.3s; }
    }
    
    &:hover .read-icon { transform: scale(1); }
  }

  .card-info {
    padding: 1.2rem;
    
    h3 {
      font-size: 1.1rem; font-weight: 700; margin-bottom: 0.5rem;
      white-space: nowrap; overflow: hidden; text-overflow: ellipsis;
      color: var(--primary-text);
    }

    .summary {
      font-size: 0.9rem; opacity: 0.7; line-height: 1.4;
      display: -webkit-box; -webkit-line-clamp: 2; -webkit-box-orient: vertical; overflow: hidden;
    }
  }
}

/* --- FEATURES SECTION --- */
.features {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 2rem;
  background-color: var(--card-bg, rgba(0,0,0,0.02));
  border-radius: 20px;
  margin-bottom: 4rem;
}

.feature-box {
  text-align: center;
  padding: 2rem;
  transition: transform 0.3s;

  &:hover { transform: translateY(-5px); }

  .icon {
    font-size: 3rem; margin-bottom: 1rem; display: inline-block; padding: 15px;
    background: rgba(52, 152, 219, 0.1); border-radius: 50%; color: var(--accent-color);
  }

  h3 { margin-bottom: 0.5rem; font-size: 1.2rem; color: var(--primary-text); }
  p { font-size: 0.95rem; opacity: 0.8; }
}

/* --- ANIMATIONS --- */
.animate-fade-up {
  animation: fadeUp 0.8s ease-out forwards;
  opacity: 0; transform: translateY(20px);
}
.delay-1 { animation-delay: 0.2s; }
.delay-2 { animation-delay: 0.4s; }

@keyframes fadeUp { to { opacity: 1; transform: translateY(0); } }

/* --- MEDIA QUERIES --- */
@media (max-width: 768px) {
  .hero-content h1 { font-size: 2.5rem; }
  .trending-grid { grid-template-columns: repeat(auto-fill, minmax(160px, 1fr)); }
  .novel-card-modern .card-image { height: 240px; }
}
</style>