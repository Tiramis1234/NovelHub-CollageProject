<template>
  <div class="comments-section">
    <h3>Komentarze ({{ comments.length }})</h3>

    <div class="add-comment-placeholder">
      <p>🔒 <router-link to="/login">Zaloguj się</router-link>, aby dodać komentarz.</p>
    </div>

 
    <div v-if="loading" class="loading">Ładowanie komentarzy...</div>
    <div v-else-if="errorMessage" class="error">{{ errorMessage }}</div>


    <div v-else-if="comments.length === 0" class="no-comments">
      Bądź pierwszy i skomentuj!
    </div>

 
    <div v-else class="comments-list">
    
      <div 
        v-for="comment in rootComments" 
        :key="comment.id" 
        class="comment-item"
      >
      
        <div class="comment-header">
          <span class="author">{{ comment.username }}</span>
          <span class="date">{{ formatDate(comment.createdAt) }}</span>
        </div>
        <div class="comment-content">{{ comment.content }}</div>

  
        <div v-if="getReplies(comment.id).length > 0" class="replies-list">
          <div 
            v-for="reply in getReplies(comment.id)" 
            :key="reply.id" 
            class="comment-item reply"
          >
            <div class="comment-header">
              <span class="author">{{ reply.username }}</span>
              <span class="date">{{ formatDate(reply.createdAt) }}</span>
            </div>
            <div class="comment-content">{{ reply.content }}</div>
          </div>
        </div>

      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue'; 
import { useRoute } from 'vue-router';

const API_URL = "http://localhost:5193"; 
const route = useRoute();

const comments = ref([]);
const loading = ref(true);
const errorMessage = ref("");


const rootComments = computed(() => {
  return comments.value.filter(c => c.parentCommentId === null);
});


const getReplies = (parentId) => {
  return comments.value.filter(c => c.parentCommentId === parentId);
};

const formatDate = (dateString) => {
  if (!dateString) return "";
  const date = new Date(dateString);
  return date.toLocaleDateString() + " " + date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
};

const fetchComments = async () => {
  loading.value = true;
  errorMessage.value = "";
  

  const slug = route.params.novelName; 

  if (!slug) return;

  try {
  
    const response = await fetch(`${API_URL}/api/comments/${slug}`);

    if (!response.ok) {
      if (response.status === 404) {
        
         comments.value = [];
         return;
      }
      throw new Error("Błąd pobierania komentarzy.");
    }

    const data = await response.json();
    comments.value = data;
  } catch (err) {
    console.error(err);
    errorMessage.value = "Nie udało się załadować komentarzy.";
  } finally {
    loading.value = false;
  }
};

onMounted(() => {
  fetchComments();
});
</script>

<style lang="scss" scoped>
.comments-section {
  margin-top: 3rem;
  padding: 1rem;
  background-color: var(--body-bg);
  border-top: 1px solid rgba(128,128,128,0.2);
}

h3 {
  margin-bottom: 1.5rem;
}

.add-comment-placeholder {
  background-color: rgba(128,128,128,0.1);
  padding: 1rem;
  border-radius: 8px;
  text-align: center;
  margin-bottom: 2rem;
  
  a {
    color: var(--accent-color, #3498db);
    font-weight: bold;
    text-decoration: none;
  }
}

.comment-item {
  margin-bottom: 1.5rem;
  padding-bottom: 1rem;
  border-bottom: 1px solid rgba(128,128,128,0.1);

  &:last-child {
    border-bottom: none;
  }
}

.comment-header {
  display: flex;
  justify-content: space-between;
  margin-bottom: 0.5rem;
  font-size: 0.9rem;
}

.author {
  font-weight: bold;
  color: var(--accent-color, #3498db);
}

.date {
  color: #888;
  font-size: 0.8rem;
}

.comment-content {
  font-size: 1rem;
  line-height: 1.5;
  color: var(--body-text);
}


.replies-list {
  margin-top: 1rem;
  margin-left: 2rem;
  padding-left: 1rem;
  border-left: 2px solid rgba(128,128,128,0.2);
}

.reply {
  margin-bottom: 1rem;
  border-bottom: none;
  padding-bottom: 0;
}

.loading, .error, .no-comments {
  text-align: center;
  color: #888;
  margin: 2rem 0;
}
.error {
  color: #e74c3c;
}
</style>