import { createRouter, createWebHistory } from 'vue-router';
import Series from '../views/Series.vue';
import Privacy from '../views/Privacy.vue';
import Home from '../views/Home.vue';
import Chapter from '../views/Chapter.vue';
import Novel from '../views/Novel.vue';
import Login from '../views/Login.vue';

const routes = [
  { path: '/', component: Home },
  { path: '/series', component: Series },
  { path: '/series/:novelName', component: Novel, name: 'novel' }, 
  { path: '/series/:novelName/chapter/:chapterNumber', component: Chapter, name: 'chapter' }, 
  { path: '/login', component: Login}
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;