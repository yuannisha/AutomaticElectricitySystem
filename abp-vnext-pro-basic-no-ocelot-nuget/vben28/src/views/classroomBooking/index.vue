<template>
    <div>
      <BasicTable @register="registerTable" size="small">

      </BasicTable>
    </div>
  </template>
  
  <script lang="ts">
  import { defineComponent } from "vue";
  import { BasicTable, useTable } from "/@/components/Table";
  import { tableColumns, searchFormSchema, getAllBookingInfor,
    storeAndInsertNewInfor,bookingInWeekend,getClassroomBookingEdit,
    createClassroomBooking,updateClassroomBooking,deleteClassroomBooking,
    identityVerify} from "/@/views/classroomBooking/index";
  
  export default defineComponent({
    name: "ClassroomBooking",
    components: {
      BasicTable
    },
    setup() {
      // table配置
      const [registerTable, { reload }] = useTable({
        columns: tableColumns,
        formConfig: {
          labelWidth: 70,
          schemas: searchFormSchema
        //   fieldMapToTime: [
        //     ["time", ["executionBeginTime", "executionEndTime"], "YYYY-MM-DD HH:mm:ss"]
        //   ]
        },
        api: getAllBookingInfor,
        showTableSetting: true,
        useSearchForm: true,
        bordered: true,
        canResize: true,
        showIndexColumn: true,
        immediate: true,
        scroll: { x: true , y: true}
      });
  
      return {
        registerTable,
        reload
      };
    }
  });
  </script>
  