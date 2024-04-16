<script setup lang="ts">
  // import { leftBottom } from "@/api";
  import SeamlessScroll from '../seamless-scroll';
  import { computed, onMounted, reactive, onUnmounted } from 'vue';
  import { useSettingStore } from '../../setting/setting';
  import { storeToRefs } from 'pinia';
  import EmptyCom from '../empty-com';
  import { ElMessage } from 'element-plus';
  import { GetAllPowerSwitchs } from '../../../../powerSwitchs';
  import { GetAllRoomsUnConditional } from '../../../../rooms';

  const settingStore = useSettingStore();
  const { defaultOption, indexConfig } = storeToRefs(settingStore);
  // let data: any;
  // const datas = reactive(data);

  const state = reactive<any>({
    list: null,
    defaultOption: {
      ...defaultOption.value,
      singleHeight: 256,
      limitScrollNum: 4,
    },
    scroll: true,
  });

  onMounted(() => {
    const getPowerSwitchsNum = setInterval(async () => {
      var powerSwitchs = await GetAllPowerSwitchs();
      var rooms = await GetAllRoomsUnConditional();
      powerSwitchs.forEach((x) => {
        rooms.forEach((y) => {
          if (x.roomId == y.id) {
            x.roomId = y.no;
          }
        });
      });
      console.log(powerSwitchs);
      state.list = powerSwitchs;
    }, 2000);

    onUnmounted(() => {
      clearInterval(getPowerSwitchsNum);
    });
  });

  // const getData = () => {
  //   leftBottom( { limitNum: 20 })
  //     .then((res) => {
  //       console.log("左下--设备提醒", res);
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
  const addressHandle = (item: any) => {
    let name = item.provinceName;
    if (item.cityName) {
      name += '/' + item.cityName;
      if (item.countyName) {
        name += '/' + item.countyName;
      }
    }
    return name;
  };
  const comName = computed(() => {
    if (indexConfig.value.leftBottomSwiper) {
      return SeamlessScroll;
    } else {
      return EmptyCom;
    }
  });
  // onMounted(() => {
  //   // getData();
  // });
</script>

<template>
  <div
    class="left_boottom_wrap beautify-scroll-def"
    :class="{ 'overflow-y-auto': !indexConfig.leftBottomSwiper }"
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
      <ul class="left_boottom">
        <li class="left_boottom_item" v-for="(item, i) in state.list" :key="i">
          <span class="orderNum doudong">{{ i + 1 }}</span>
          <div class="inner_right">
            <div class="dibu"></div>
            <div class="flex">
              <div class="info">
                <span class="labels">序列号：</span>
                <span class="text-content zhuyao doudong wangguan"> {{ item.serialNumber }}</span>
              </div>
              <div class="info">
                <span class="labels">控制器械名：</span>
                <span class="text-content" style="font-size: 12px">
                  {{ item.controlledMachineName }}</span
                >
              </div>
            </div>

            <span
              class="types doudong"
              :class="{
                typeRed: item.isOnline == 0,
                typeGreen: item.isOnline == 1,
              }"
              >{{ item.isOnline == 1 ? '在线' : '离线' }}
            </span>

            <div class="info addresswrap">
              <span class="labels">所属教室：</span>
              <!-- <span class="text-content ciyao" style="font-size: 12px">
                {{ addressHandle(item) }}</span
              > -->
              {{ item.roomId }}
              <span
                class="types doudong rightMargin"
                :class="{
                  typeRed: item.status == 0,
                  typeGreen: item.status == 1,
                }"
                >{{ item.status == 1 ? '开闸' : '合闸' }}
              </span>

              <span
                class="types doudong rightMargin"
                :class="{
                  typeRed: item.isAbnormal == 1,
                  typeGreen: item.isAbnormal == 0,
                }"
                >{{ item.isAbnormal == 1 ? '异常' : '正常' }}
              </span>
            </div>
          </div>
        </li>
      </ul>
    </component>
  </div>
</template>

<style scoped lang="scss">
  .left_boottom_wrap {
    overflow: hidden;
    width: 100%;
    height: 100%;
  }

  .doudong {
    overflow: hidden;
    backface-visibility: hidden;
  }

  .overflow-y-auto {
    overflow-y: auto;
  }

  .left_boottom {
    width: 100%;
    height: 100%;

    .left_boottom_item {
      display: flex;
      align-items: center;
      justify-content: center;
      padding: 8px;
      font-size: 14px;
      // margin: 10px 0;
      .orderNum {
        margin: 0 16px 0 -20px;
      }

      .info {
        margin-right: 10px;
        display: block;
        align-items: center;
        color: #fff;

        .labels {
          flex-shrink: 0;
          font-size: 12px;
          color: rgba(255, 255, 255, 0.6);
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

      .inner_right {
        position: relative;
        height: 100%;
        width: 380px;
        flex-shrink: 0;
        line-height: 1;
        display: flex;
        align-items: center;
        justify-content: space-between;
        flex-wrap: wrap;
        .dibu {
          position: absolute;
          height: 2px;
          width: 104%;
          background-image: url('../../assets/img/zuo_xuxian.png');
          bottom: -10px;
          left: -2%;
          background-size: cover;
        }
        .addresswrap {
          width: 100%;
          display: flex;
          margin-top: 8px;
        }
      }

      .wangguan {
        color: #1890ff;
        font-weight: 900;
        font-size: 15px;
        width: 80px;
        flex-shrink: 0;
      }

      .time {
        font-size: 12px;
        // color: rgba(211, 210, 210,.8);
        color: #fff;
      }

      .address {
        font-size: 12px;
        cursor: pointer;
        // @include text-overflow(1);
      }

      .types {
        width: 30px;
        flex-shrink: 0;
      }
      .rightMargin{
        margin-left:auto;
      }
      .typeRed {
        color: #fc1a1a;
      }

      .typeGreen {
        color: #29fc29;
      }
    }
  }
</style>
