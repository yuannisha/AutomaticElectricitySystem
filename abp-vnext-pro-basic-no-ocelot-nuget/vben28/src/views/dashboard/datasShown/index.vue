<script setup lang="ts">
  import { ref } from 'vue';
  import ScaleScreen from './compoent/scale-screen/scale-screen.vue';
  import Headers from './compoent/header/header.vue';
  import { useSettingStore } from './setting/setting';
  import { storeToRefs } from 'pinia';
  import AllTables from './compoent/allCharts/index.vue';

  const settingStore = useSettingStore();
  const { isScale } = storeToRefs(settingStore);
  const wrapperStyle = {};
</script>

<template>
  <scale-screen
    width="1920"
    height="1080"
    :delay="500"
    :fullScreen="true"
    :boxStyle="{
      background: '#03050C',
      overflow: isScale ? 'hidden' : 'auto',
    }"
    :wrapperStyle="wrapperStyle"
    :autoScale="isScale"
  >
    <div class="content_wrap">
      <Headers />
      <AllTables />
    </div>
  </scale-screen>
</template>
<style lang="scss" scoped>
  .content_wrap {
    width: 100%;
    height: 100%;
    padding: 16px 16px 16px 16px;
    box-sizing: border-box;
    background-image: url('./assets/img/pageBg.png');
    background-size: cover;
    background-position: center center;
  }
</style>
