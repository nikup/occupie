				function check_eik(eik_str){
	eik_str = eik_str.replace(/\s+/, '');
	eik_len = eik_str.length;
	if((eik_len == 9) || (eik_len == 13)){
		eik = parseInt(eik_str);
		if(isNaN(eik)){
			alert('Грешен ЕИК номер! Моля използвайте само цифри!');
			return false;
		}else{
			sum = 0;
			for(var i = 0; i < 8; i++){
				sum += eik_str.charAt(i)*(i+1);
			}
			new_value = sum % 11;
			if(new_value == 10){
				sum = 0;
				for(i = 0; i < 8; i++){
					sum += eik_str.charAt(i)*(i+3);
				}
				new_value = sum % 11;
				if(new_value == 10){
					new_value = 0;
				}
			}
 
			if(new_value == eik_str.charAt(8)){
				if (eik_len == 9){
					alert('Въведеният ЕИК е валиден!');
				}else{
					sum = eik_str.charAt(8)*2 + eik_str.charAt(9)*7 + eik_str.charAt(10)*3 + eik_str.charAt(11)*5;
					new_value = sum % 11;
					if(new_value == 10){
						sum = eik_str.charAt(8)*4 + eik_str.charAt(9)*9 + eik_str.charAt(10)*5 + eik_str.charAt(11)*7;
						new_value = sum % 11;
						if(new_value == 10){
							new_value = 0;
						}
					}
					if(new_value == eik_str.charAt(12)){
						alert('Въведеният ЕИК е валиден!');
					}else{
						alert('Въведеният ЕИК е невалиден!');
						return false;
					}
				}
			}else{
				alert('Въведеният ЕИК е невалиден!');
				return false;
			}
		}
		}else{
		alert('Грешен ЕИК');
		return false;
	}
}