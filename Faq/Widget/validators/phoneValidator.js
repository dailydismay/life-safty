export default function validatePhone(phone){
    const re = /^\d[\d\(\)\ -]{4,14}\d$/;
    return re.test(String(phone).toLowerCase());
}