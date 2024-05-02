<script setup lang="ts">
  // import { rightBottom } from "@/api";
  import SeamlessScroll from '../seamless-scroll';
  import { computed, onMounted, reactive, onUnmounted } from 'vue';
  import { useSettingStore } from '../../setting/setting';
  import { storeToRefs } from 'pinia';
  import EmptyCom from '../empty-com';
  import { ElMessage } from 'element-plus';
  import { getAllInformation } from '../../../../classroomBooking';

  const settingStore = useSettingStore();
  const { defaultOption, indexConfig } = storeToRefs(settingStore);
  const state = reactive<any>({
    list: [],
    defaultOption: {
      ...defaultOption.value,
      singleHeight: 252,
      limitScrollNum: 3,
      // step:3
    },
    scroll: true,
  });

  onMounted(() => {
    const getPowerSwitchsNum = setInterval(async () => {
      var bookingInformation = await getAllInformation();
      console.log(bookingInformation);
      state.list = bookingInformation;
    }, 2000);

    onUnmounted(() => {
      clearInterval(getPowerSwitchsNum);
    });
  });

  // const getData = () => {
  //   rightBottom({ limitNum: 20 })
  //     .then((res) => {
  //       console.log("右下", res);
  //       if (res.success) {
  //         state.list = res.data.list;
  //       } else {
  //         ElMessage({
  //           message: res.msg,
  //           type: "warning",
  //         });
  //       }
  //     })
  //     .catch((err) => {
  //       ElMessage.error(err);
  //     });
  // };

  const comName = computed(() => {
    if (indexConfig.value.rightBottomSwiper) {
      return SeamlessScroll;
    } else {
      return EmptyCom;
    }
  });
  function montionFilter(val: any) {
    // console.log(val);
    return val ? Number(val).toFixed(2) : '--';
  }
  const handleAddress = (item: any) => {
    return `${item.provinceName}/${item.cityName}/${item.countyName}`;
  };
  // onMounted(() => {
  //   // getData();
  //   state.list = data;
  // });
</script>

<template>
  <div
    class="right_bottom_wrap beautify-scroll-def"
    :class="{ 'overflow-y-auto': !indexConfig.rightBottomSwiper }"
  >
    <component
      :is="comName"
      :list="state.list"
      v-model="state.scroll"
      :singleHeight="state.defaultOption.singleHeight"
      :step="state.defaultOption.step"
      :limitScrollNum="state.defaultOption.limitScrollNum"
      :hover="state.defaultOption.hover"
      :singleWaitTime="state.defaultOption.singleWaitTime"
      :wheel="state.defaultOption.wheel"
    >
      <ul class="right_bottom">
        <li class="right_center_item" v-for="(item, i) in state.list" :key="i">
          <span class="orderNum">{{ i + 1 }}</span>
          <div class="inner_right">
            <div class="dibu"></div>
            <div class="flex">
              <div class="info first">
                <span class="labels">学号：</span>
                <span class="text-content zhuyao"> {{ item.studentId }}</span>
              </div>
              <div class="info first">
                <span class="labels">姓名：</span>
                <span class="text-content"> {{ item.studentName }}</span>
              </div>
              <div class="info first">
                <span class="labels">班级：</span>
                <span class="text-content warning"> {{ item.studentClass }}</span>
              </div>
            </div>

            <div class="flex">
              <div class="info flexChild1">
                <span class="labels shrink-0"> 使用用途：</span>
                <span class="truncate ciyao" style="font-size: 12px; width: 220px">
                  {{ item.usingPurpose }}</span
                >
              </div>
              <div class="info time shrink-0 flexChild2">
                <span class="labels">申请时间段：</span>
                <span class="text-content text-adjust" style="font-size: 12px">
                  {{ item.bookingTimespan }}</span
                >
              </div>
            </div>
            <div class="flex">
              <div class="info">
                <span class="labels">申请教室：</span>
                <span class="text-content ciyao" :class="{ warning: item.alertdetail }">
                  {{ item.usingClassroom || '无' }}</span
                >
              </div>
            </div>
          </div>
        </li>
      </ul>
    </component>
  </div>
</template>

<style scoped lang="scss">
  .right_bottom {
    width: 100%;
    height: 100%;

    .right_center_item {
      height: auto;
      padding: 10px;
      font-size: 14px;
      color: #fff;
      margin: 20px 50px;
      display: flex;
      align-items: center;

      .orderNum {
        margin: 0 20px 0 -20px;
      }

      .inner_right {
        position: relative;
        height: 100%;
        width: 400px;
        flex-shrink: 0;
        line-height: 1.5;

        .dibu {
          position: absolute;
          height: 2px;
          width: 104%;
          background-image: url('../../assets/img/zuo_xuxian.png');
          bottom: -12px;
          left: -2%;
          background-size: cover;
        }
        .flex {
          display: flex;
          align-items: center;
          justify-content: center;
          width: 100%;
        }
      }

      .info {
        // margin-right: 10px;
        // display: flex;
        // align-items: center;

        .labels {
          flex-shrink: 0;
          font-size: 12px;
          color: rgba(255, 255, 255, 0.6);
        }
        .text-adjust {
        }

        .zhuyao {
          color: #1890ff;
          font-size: 15px;
        }

        .ciyao {
          color: rgba(255, 255, 255, 0.8);
        }

        .warning {
          color: #e6a23c;
          font-size: 15px;
        }
      }
      .first {
        flex: 1;
      }
      .flexChild1 {
        flex: 1;
      }
      .flexChild2 {
        flex: 3;
      }
    }
  }

  .right_bottom_wrap {
    overflow: hidden;
    width: 100%;
    height: 252px;
  }

  .overflow-y-auto {
    overflow-y: auto;
  }
</style>
