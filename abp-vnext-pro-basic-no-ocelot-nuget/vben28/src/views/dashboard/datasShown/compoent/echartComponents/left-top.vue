<script setup lang="ts">
  import { reactive, ref, onMounted, onUnmounted } from 'vue';
  // import { countDeviceNum } from "../../api";
  import CountUp from '../count-up';
  // import {ElMessage} from "element-plus"
  import { GetAllBuildingsUnConditional } from '../../../../buildings';
  import { GetAllRoomsUnConditional } from '../../../../rooms';
  import { GetAllPowerSwitchs } from '../../../../powerSwitchs';

  const duration = ref(2);
  const state = reactive({
    buildingsNum: 0,
    roomsNum: 0,
    powerSwitchsNum: 0,
  });

  onMounted(() => {
    const getBuildingsNum = setInterval(async () => {
      var buildings = await GetAllBuildingsUnConditional();
      if (state.buildingsNum != buildings.length) {
        state.buildingsNum = buildings.length;
      }
    }, 2000);

    const getRoomsNum = setInterval(async () => {
      var rooms = await GetAllRoomsUnConditional();
      if (state.roomsNum != rooms.length) {
        state.roomsNum = rooms.length;
      }
    }, 2000);

    const getPowerSwitchsNum = setInterval(async () => {
      var powerSwitchs = await GetAllPowerSwitchs();
      if (state.powerSwitchsNum != powerSwitchs.length) {
        state.powerSwitchsNum = powerSwitchs.length;
      }
    }, 2000);

    onUnmounted(() => {
      clearInterval(getBuildingsNum);
      clearInterval(getRoomsNum);
      clearInterval(getPowerSwitchsNum);
    });
  });

  // const getData = () => {
  //   countDeviceNum().then((res) => {
  //     console.log("左上--设备总览",res);
  //     if (res.success) {
  //       state.alarmNum = res.data.alarmNum;
  //       state.offlineNum = res.data.offlineNum;
  //       state.onlineNum = res.data.onlineNum;
  //       state.totalNum = res.data.totalNum;
  //     }else{
  //       ElMessage.error(res.msg)
  //     }
  //   }).catch(err=>{
  //     ElMessage.error(err)
  //   });;
  // state.buildingsNum = 40;
  // state.roomsNum = 658;
  // state.powerSwitchsNum = 698;
  // };
  // getData();
</script>

<template>
  <ul class="flex user_Overview">
    <li class="user_Overview-item" style="color: #00fdfa">
      <div class="user_Overview_nums allnum">
        <CountUp :endVal="state.buildingsNum" :duration="duration" />
      </div>
      <p>总楼栋数</p>
    </li>
    <li class="user_Overview-item" style="color: #07f7a8">
      <div class="user_Overview_nums online">
        <CountUp :endVal="state.roomsNum" :duration="duration" />
      </div>
      <p>总教室数</p>
    </li>
    <li class="user_Overview-item" style="color: #e3b337">
      <div class="user_Overview_nums offline">
        <CountUp :endVal="state.powerSwitchsNum" :duration="duration" />
      </div>
      <p>总空开数</p>
    </li>
  </ul>
</template>

<style scoped lang="scss">
  .left-top {
    width: 100%;
    height: 100%;
  }

  .user_Overview {
    li {
      flex: 1;

      p {
        text-align: center;
        height: 16px;
        font-size: 16px;
      }

      .user_Overview_nums {
        width: 100px;
        height: 100px;
        text-align: center;
        line-height: 100px;
        font-size: 22px;
        margin: 50px auto 30px;
        background-size: cover;
        background-position: center center;
        position: relative;

        &::before {
          content: '';
          position: absolute;
          width: 100%;
          height: 100%;
          top: 0;
          left: 0;
        }

        &.bgdonghua::before {
          animation: rotating 14s linear infinite;
        }
      }

      .allnum {
        &::before {
          background-image: url('../../assets/img/left_top_lan.png');
        }
      }

      .online {
        &::before {
          background-image: url('../../assets/img/left_top_lv.png');
        }
      }

      .offline {
        &::before {
          background-image: url('../../assets/img/left_top_huang.png');
        }
      }

      .laramnum {
        &::before {
          background-image: url('../../assets/img/left_top_hong.png');
        }
      }
    }
  }
</style>
