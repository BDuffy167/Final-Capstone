<script>
import {Bar} from "vue-chartjs";

export default {
    extends: Bar,
    props:{
        label:{
            type: String
        },
        chartData:{
            type: Array
        },
        options: {
            type: Object
        }
    },
    mounted(){
      
    }
}
</script>