<template>
  <div class="home">
    <b-carousel
      id="carousel-1"
      v-model="slide"
      :interval="4000"
      controls
      indicators
      background="#ababab"
      img-width="1024"
      img-height="480"
      style="text-shadow: 1px 1px 2px #333;"
      @sliding-start="onSlideStart"
      @sliding-end="onSlideEnd"
    >
      <b-carousel-slide 
      caption="Zarejestruj swoje auto w dosłownie kilku krokach"
      text="Rejestracja jest banalnie prosta"
      >
        <template #img>
          <img
            class="d-block img-fluid w-100"
            width="1024"
            height="480"
            src="@/assets/ferari.png"
            alt="image slot"
          >
        </template>
      </b-carousel-slide>


      <b-carousel-slide 
      caption="Uzupełnij formularz rejestracyjny już teraz"
      text=""
      >
        <template #img>
          <img
            class="d-block img-fluid w-100"
            width="1024"
            height="480"
            src="@/assets/auto_2.png"
            alt="image slot"
          >
        </template>
      </b-carousel-slide>


      <b-carousel-slide 
      caption="Pamiętaj aby zarejestrować auto do 30 dni po zakupie"
      text="Posiadanie niezarejstrowanego auta jest nielegalne"
      >
        <template #img>
          <img
            class="d-block img-fluid w-100"
            width="1024"
            height="480"
            src="@/assets/auto_4.jpg"
            alt="image slot"
          >
        </template>
      </b-carousel-slide>

    </b-carousel>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        slide: 0,
        sliding: null
      }
    },
    methods: {
      onSlideStart(slide) {
        console.log(slide)
        this.sliding = true
      },
      onSlideEnd(slide) {
        console.log(slide)
        this.sliding = false
      }
    }
  }
</script>
<style>

.home{
  height: 100%;
  /* top: 56px; */
  width: 100%;
  /* margin: 10px; */
  top: 36px;
  z-index: -999;
  position: absolute;
}
::-webkit-scrollbar {
    display: none;
}
</style>