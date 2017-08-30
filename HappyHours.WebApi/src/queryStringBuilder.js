
export default function(obj) {
  let str = '';
  for(var key in obj) {
    str += key + '=' + obj[key] + '&';
  }
  return str.substring(0, str.length - 1);
}
