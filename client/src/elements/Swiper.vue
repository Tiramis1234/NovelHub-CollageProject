<template>
  <div class="carousel-wrapper">
    <swiper
      :modules="modules"
      :slides-per-view="1"
      :space-between="20"
      :navigation="true"
      :pagination="{ clickable: true }"
      :breakpoints="breakpoints"
      :loop="true"
      @swiper="onSwiper"
      @slideChange="onSlideChange"
    >
      <swiper-slide v-for="(slide, index) in slides" :key="index">
        <div class="slide-content">
          <h3>{{ slide.title }}</h3>
          <p>{{ slide.description }}</p>
        </div>
      </swiper-slide>
    </swiper>
  </div>
</template>

<script setup>

import { Swiper, SwiperSlide } from 'swiper/vue';

import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';

import { Navigation, Pagination, A11y } from 'swiper/modules';
import { ref } from 'vue';

const modules = [Navigation, Pagination, A11y];


const slides = ref([
  { title: 'Slajd 1', description: 'Opis pierwszego slajdu' },
  { title: 'Slajd 2', description: 'Opis drugiego slajdu' },
  { title: 'Slajd 3', description: 'Opis trzeciego slajdu' },
  { title: 'Slajd 4', description: 'Opis czwartego slajdu' },
  { title: 'Slajd 5', description: 'Opis piątego slajdu' },
]);

const breakpoints = {

  640: {
    slidesPerView: 2,
    spaceBetween: 20,
  },

  1024: {
    slidesPerView: 3,
    spaceBetween: 30,
  },
};

const onSwiper = (swiper) => {
  console.log('Swiper instance:', swiper);
};

const onSlideChange = () => {
  console.log('Zmieniono slajd');
};
</script>

<style lang="scss">
.slide-content {
  background-color: #f3f4f6;
  border-radius: 10px;
  height: 200px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  text-align: center;
  border: 1px solid #ddd;
}

.carousel-wrapper {
  max-width: 1000px;
  margin: 0 auto;
  padding: 40px 0;
}


:deep(.swiper-button-next),
:deep(.swiper-button-prev) {
  color: #42b883;
}

:deep(.swiper-pagination-bullet-active) {
  background-color: #42b883;
}
</style>