<template>
  <div class="center-header-wrapper">
    <div class="item-wrapper">
      <div class="center-header-item" v-for="(item, index) in headerData1" :key="index">
        <div class="left">
          <!-- <div class="bg" :style="{ backgroundImage: `url('${item.img}')` }"> -->
          <img class="bg" src="../../../../../assets/images/allEnergyConsumption.png" alt="" />
          <!-- </div> -->
        </div>
        <div class="right">
          <div class="title">{{ item.title }}</div>
          <div class="subtitle">{{ item.subTitle }}</div>
          <div class="total">
            {{ item.Val }}
          </div>
        </div>
      </div>
    </div>
    <div class="item-wrapper">
      <div class="center-header-item" v-for="(item, index) in headerData2" :key="index">
        <div class="left">
          <!-- <div class="bg">
            <div class="img" :style="{ backgroundImage: `url('${item.img}')` }"></div>
          </div> -->
          <img class="bg" src="../../../../../assets/images/allEnergyConsumption.png" alt="" />
        </div>
        <div class="right">
          <div class="title">{{ item.title }}</div>
          <div class="subtitle">{{ item.subTitle }}</div>
          <div class="total">
            {{ item.Val }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
  //   import { countToDuration } from "@/const";
  // const countToDuration = 1000;
  import { ref, watch, onMounted, onUnmounted } from 'vue';
  import { BuildingConsumptionServiceProxy } from '/@/services/ServiceProxies';

  // const props = withDefaults(
  //   defineProps<{
  //     data: any;
  //   }>(),
  //   {},
  // );

  const headerData1 = ref<any[]>([]);
  const headerData2 = ref<any[]>([]);
  const initData1 = (datas: any) => {
    headerData1.value = [
      {
        title: '今日电量消耗(度)',
        subTitle: "Today's Sales Amount",
        // startVal: (oldProps && oldProps.salesToday) || 0,
        // endVal: newProps.salesToday,
        Val: datas.todayConsumption,
        img: '../../../../../assets/images/allEnergyConsumption.png',
      },
      {
        title: '今日申请教室人数(人)',
        subTitle: "Today's Total Orders",
        // startVal: (oldProps && oldProps.orderToday) || 0,
        // endVal: newProps.orderToday,
        Val: datas.todayStudentsOfBooking,
        img: 'https://www.youbaobao.xyz/datav-res/order.png',
      },
      {
        title: '今日已申请教室数(间)',
        subTitle: "Today's Payed Users",
        // startVal: (oldProps && oldProps.orderUser) || 0,
        // endVal: newProps.orderUser,
        Val: datas.todayRoomsBeBooked,
        img: 'https://www.youbaobao.xyz/datav-res/member.png',
      },
    ];
  };
  const initData2 = (datas: any) => {
    headerData2.value = [
      {
        title: '总电量消耗(度)',
        subTitle: "Today's Sales Amount",
        // startVal: (oldProps && oldProps.salesToday) || 0,
        // endVal: newProps.salesToday,
        Val: datas.totalConsumption,
        img: '../../../../../assets/images/allEnergyConsumption.png',
      },
      {
        title: '总申请教室人数(人)',
        subTitle: "Today's Total Orders",
        // startVal: (oldProps && oldProps.orderToday) || 0,
        // endVal: newProps.orderToday,
        Val: datas.totalRoomsBeBooked,
        img: 'https://www.youbaobao.xyz/datav-res/order.png',
      },
      {
        title: '总已申请教室数(间)',
        subTitle: "Today's Payed Users",
        // startVal: (oldProps && oldProps.orderUser) || 0,
        // endVal: newProps.orderUser,
        Val: datas.totalStudentsOfBooking,
        img: 'https://www.youbaobao.xyz/datav-res/member.png',
      },
    ];
  };
  const getDatasForData = async () => {
    const service = new BuildingConsumptionServiceProxy();
    var datas = await service.getTodayDatasCount();
    console.log(datas);
    initData1(datas);
    initData2(datas);
  };
  onMounted(async () => {
    await getDatasForData();
    const ttt = setInterval(getDatasForData, 1000 * 10);
    onUnmounted(() => {
      clearInterval(ttt);
    });
  });
  //   watch(
  //     () => props.data,
  //     (newProps, oldProps) => {
  //       initData(newProps, oldProps);
  //     },
  //     {
  //       immediate: true,
  //     }
  //   );
  // onMounted(() => {
  //   initData(0, 0);
  // });
</script>

<style lang="less" scoped>
  .center-header-wrapper {
    display: flex;
    align-items: center;
    width: 90%;
    height: 100%;
    flex-direction: row;

    .item-wrapper {
      display: flex;
      flex-direction: column;
      .center-header-item {
        flex: 1;
        display: flex;
        margin: 40px 0px;
        .left {
          display: flex;
          align-items: center;
          .bg {
            display: flex;
            align-items: center;
            justify-content: center;
            width: 100px;
            height: 100px;
            background-repeat: no-repeat;
            background-size: 100%;
            // background-color: #e6a23c;
            border-radius: 50%;

            .img {
              width: 50%;
              height: 50%;
              border-radius: 50%;
              background-repeat: no-repeat;
              background-size: 100% 100%;
            }
          }
        }

        .right {
          flex: 1;
          display: flex;
          flex-direction: column;
          justify-content: center;
          margin-left: 40px;
          width: 180px;

          .title {
            font-size: 18px;
          }

          .sub-title {
            font-size: 12px;
            letter-spacing: 1px;
            margin-top: 10px;
          }

          .total {
            font-size: 32px;
            font-weight: 500;
            letter-spacing: 2px;
            margin-top: 10px;
          }
        }
      }
    }
  }
</style>
