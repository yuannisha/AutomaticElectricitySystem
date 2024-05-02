<script setup lang="ts">
  import ItemWrap from '../item-wrap';
  import LeftTop from '../echartComponents/left-top.vue';
  import LeftCenter from '../echartComponents/left-center.vue';
  import LeftBottom from '../echartComponents/left-bottom.vue';
  // import CenterMap from "../echartComponents/center-map.vue";
  import CenterBottom from "../echartComponents/center-bottom.vue";
  import RightTop from '../echartComponents/right-top.vue';
  import RightCenter from '../echartComponents/right-center.vue';
  import RightBottom from '../echartComponents/right-bottom.vue';
  import Center from '../echartComponents/center.vue';
</script>

<template>
  <div class="index-box">
    <div class="contetn_left">
      <ItemWrap class="contetn_left-top contetn_lr-item" title="设备总览">
        <LeftTop />
      </ItemWrap>
      <ItemWrap class="contetn_left-center contetn_lr-item" title="空开总览">
        <LeftCenter />
      </ItemWrap>
      <ItemWrap
        class="contetn_left-bottom contetn_lr-item"
        title="空开列表"
        style="padding: 0 10px 16px 10px"
      >
        <LeftBottom />
      </ItemWrap>
    </div>
    <div class="contetn_center">
      <Center class="contetn_center_top" title="数据概览" />
      <ItemWrap class="contetn_center-bottom" title="教室使用情况">
        <CenterBottom />
      </ItemWrap>
    </div>
    <div class="contetn_right">
      <ItemWrap class="contetn_left-bottom contetn_lr-item" title="总电量消耗/近七天/度°">
        <RightTop />
      </ItemWrap>
      <ItemWrap
        class="contetn_left-bottom contetn_lr-item"
        title="各楼栋电量消耗排名"
        style="padding: 0 10px 16px 10px"
      >
        <RightCenter />
      </ItemWrap>
      <ItemWrap class="contetn_left-bottom contetn_lr-item" title="教室预约列表 ">
        <RightBottom />
      </ItemWrap>
    </div>
  </div>
</template>

<style scoped lang="scss">
  .index-box {
    width: 100%;
    display: flex;
    min-height: calc(100% - 64px);
    justify-content: space-between;
  }
  //左边 右边 结构一样
  .contetn_left,
  .contetn_right {
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    position: relative;
    width: 540px;
    box-sizing: border-box;
    flex-shrink: 0;
  }
  .contetn_center {
    width:340px;
    flex: 1;
    margin: 0 54px;
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    .contetn_center-bottom {
      height: 315px;
    }
  }

  .contetn_lr-item {
    height: 310px;
  }
</style>
